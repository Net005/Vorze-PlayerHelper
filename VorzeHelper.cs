namespace Vorze_PlayerHelper
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.IO;
  using System.IO.Ports;
  using System.Runtime.InteropServices;
  using System.Text;
  using System.Text.RegularExpressions;
  using System.Threading;

  using Cyclone2.Devices;
  using Libraries.Telnet;

  public class VorzeHelper
  {
    // ZOOM PLAYER
    private static TelnetConnection tc;
    private static bool zoomPlayerIsActive;
    private static int currentPlayPositionMovie;
    private static string zoomPlayerIP = Properties.Settings.Default.ZplayerIP;
    private static int zoomPlayerPort = int.Parse(Properties.Settings.Default.ZplayerPORT);

    // PLAYER STATUS
    private static bool playerWasInactive;
    private static bool playerInactive;
    private static bool playerPauseEnabled;
    public static bool vorzeIsEnabled;
    private static string csvFileName = "";
    private static string currentlyActiveCSV;

    // VORZE DEVICE
    private static SerialPort serialPort;
    private static UsbDongle vorzeDongle;
    private static byte[] dataBuf = new byte[3];
    private static readonly object syncObject = new object();
    private static bool changeCOMDevice;
    private static HashSet<string> vorzeDeviceNames;

    // VORZE CSV DATA
    private static ArrayList csvfileData = new ArrayList();
    private static ComType comFileData;

    // VORZE COUNTERS
    private static DateTime lastCmdSend;
    private static uint vorzeCounter;
    private static uint vorzePre_counter;
    private static int vorzeNext;

    private static System.Windows.Forms.Timer timer;

    public static void Init()
    {
      try
      {
        logger("Initializing Vorze USB dongle..", true);
        changeCOMDevice = true;
        serialPort = new SerialPort();
        serialPort.BaudRate = 19200;

        timer = new System.Windows.Forms.Timer();
        timer.Interval = 20;
        timer.Tick += timer_Tick;

        InitialCommand();

        vorzeDeviceNames = LoaddeviceNames();

        UsbDongle newDongle = discoverUsbDongle();

        if (newDongle != null)
        {
          vorzeDongle = newDongle;
        }
        changeCOMDevice = true;
        timer.Enabled = true;
        timer.Stop();
        logger("initialization of Vorze USB dongle completed successfully.", true);
      }
      catch (Exception e)
      {
        logger("Error during Vorze USB dongle initialization, make sure the Vorze USB dongle is connected and installed properly.", false);
        logger(e.Message, true);
      }
    }

    public static void startPlayer()
    {
      logger("Starting player..", false);
      vorzeIsEnabled = true;
      zoomPlayerIsActive = true;
      playerInactive = false;
      playerWasInactive = false;

      Thread t = new Thread(playerInformationRetrieval);
      t.IsBackground = true;
      t.Start();

      resetCommand();
      Thread.Sleep(100);
      timer.Start();
      Thread.Sleep(100);

      if (vorzeDongle != null)
      {
        try
        {
          openComPort();
          lastCmdSend = DateTime.Now;
        }
        catch (Exception)
        {
        }
      }
      logger("Started player", false);
    }
    public static void restartPlayer()
    {
      vorzeIsEnabled = true;
      resetCommand();
      logger("Restarting player..", false);
      Thread.Sleep(100);

      if (vorzeDongle != null)
      {
        try
        {
          openComPort();
          lastCmdSend = DateTime.Now;
        }
        catch (Exception)
        {
        }
      }
      logger("Restarted player", false);
    }

    public static void stopPlayer()
    {
      vorzeIsEnabled = false;
      playerPauseEnabled = false;
      playerInactive = false;
      playerWasInactive = false;

      currentlyActiveCSV = "";
      csvFileName = "";
      currentPlayPositionMovie = 0;

      timer.Stop();
      try
      {
        closeComPort();
      }
      catch (Exception)
      {
      }
    }

    public static void playerInformationRetrieval()
    {
      while (vorzeIsEnabled)
      {
        try
        {
          tc = new TelnetConnection(zoomPlayerIP, zoomPlayerPort);
          char[] charSeparator = new char[] { ' ' };

          // Disable live playback reporting
          if (tc.IsConnected)
          {
            tc.Write("1100 0" + Environment.NewLine);
          }
          Thread.Sleep(1000);

          while (tc.IsConnected)
          {
            try
            {
              // Check if playing
              tc.Write("1000" + Environment.NewLine);
              Thread.Sleep(10);

              string playing = (tc.Read());

              if (playing.StartsWith("1000 "))
              {
                string[] playingSplit = playing.Split(charSeparator);
                playing = playingSplit[1].Trim();

                if (playing != "3")
                {
                  // Prevent flooding logs if already paused
                  if (!playerInactive)
                  {
                    logger("Zoom player isn't playing video file (paused / stopped)", true);
                  }
                  playerInactive = true;
                }
                else
                {
                  if (playerInactive)
                  {
                    playerWasInactive = true;
                  }

                  playerInactive = false;
                }
              }

              // If player is active then retrieve time position
              if (!playerInactive)
              {
                // Get CSV filename exists, run this every time to make sure the playback file hasn't changed
                //logger("Retrieving filename for CSV lookup", false);

                tc.Write("1800" + Environment.NewLine);
                Thread.Sleep(10);

                string filenameResult = (tc.Read());
                try
                {
                  filenameResult = filenameResult + (tc.Read());
                }
                catch (Exception)
                {
                }

                if (filenameResult.StartsWith("1800 ") && filenameResult != currentlyActiveCSV)
                {
                  csvFileName = filenameResult.Replace("1800 ", string.Empty).Trim();
                  //logger(string.Format("CSV lookup filename: {0}", csvFileName), false);

                  if (File.Exists(csvFileName))
                  {
                    zoomPlayerIsActive = true;
                    processCSV(csvFileName);
                  }
                }


                tc.Write("1120" + Environment.NewLine);

                string timecode = (tc.Read());

                if (timecode.StartsWith("1120 "))
                {
                  try
                  {
                    string[] timeCodeSplit = timecode.Split(charSeparator);
                    timecode = timeCodeSplit[1].Trim();
                    int intTimeCode;
                    bool successfullyParsed = int.TryParse(timecode, out intTimeCode);

                    if (successfullyParsed)
                    {
                      if (intTimeCode > 100)
                      {
                        intTimeCode = int.Parse(timecode) / 100;
                      }

                      currentPlayPositionMovie = intTimeCode;

                      //Debug.WriteLine("Zoom player timecode (ms): " + intTimeCode);
                    }
                  }
                  catch (Exception)
                  {
                  }
                }
              }
            }
            catch (Exception)
            {

            }
          }
        }
        catch (Exception)
        {
          //logger("No connection could be made to Zoom Player, make sure you enabled the control API in Zoom player and that the player is running", true);
          if (!playerInactive)
          {
            playerInactive = true;
          }
          Thread.Sleep(2500);
        }
      }
    }

    private static void logger(string message, bool writeLog)
    {
      Debug.WriteLine(message);
      if (writeLog)
      {
        StreamWriter sw = new StreamWriter("debug.log", true);
        sw.WriteLine(message);
        sw.Close();
      }
    }
    private static void processCSV(string filenamePath)
    {
      if (filenamePath != currentlyActiveCSV)
      {
        currentlyActiveCSV = filenamePath;
        string VorzeDBpath = Properties.Settings.Default.VorzeRootDIR;
        string filenameOnly = Path.GetFileNameWithoutExtension(filenamePath);

        string CSVfullPath = Path.Combine(VorzeDBpath, filenameOnly + ".csv");
        bool fileFound = false;
        Debug.WriteLine(CSVfullPath);

        if (File.Exists(CSVfullPath))
        {
          fileFound = true;
        }
        else if (!fileFound)
        {
          CSVfullPath = Path.Combine(VorzeDBpath, filenameOnly.Replace("-", string.Empty) + ".csv");

          if (File.Exists(CSVfullPath))
          {
            fileFound = true;
          }
          else
          {
            string movieIDwithOutExtension = Path.GetFileNameWithoutExtension(filenameOnly.Replace(".jpg", string.Empty));
            CSVfullPath = Path.Combine(VorzeDBpath, movieIDwithOutExtension + ".csv");

            if (File.Exists(CSVfullPath))
            {
              fileFound = true;
            }
          }
        }
        if (!fileFound)
        {
          logger(string.Format("Couldn't find Vorze CSV file for: {0}", filenamePath), true);
        }
        else
        {
          logger(string.Format(" Vorze CSV file located for: {0}", filenamePath), true);
          csvImport(CSVfullPath);
        }
      }
    }

    private static void csvImport(string filename)
    {
      csvfileData.Clear();

      uint num = 0;
      Regex regex = new Regex(@"^-?[0-9\.]{1,},-?[0-9\.]{1,},-?[0-9\.]{1,},?");
      try
      {
        using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
          using (TextReader reader = new StreamReader(stream, Encoding.GetEncoding("shift-jis")))
          {
            string str;
            while ((str = reader.ReadLine()) != null)
            {
              num++;
              if (!regex.IsMatch(str))
              {
                Debug.WriteLine("line {0} is invalid data [{1}]", num, str);
              }
              else
              {
                csvfileData.Add(str);
              }
            }
          }
        }
        resetCommand();
        logger("Vorze CSV file imported", false);
      }
      catch (Exception exception)
      {
        logger("Vorze CSV file could not be read:", true);
        Debug.WriteLine(exception.Message);
      }
    }

    private static void closeComPort()
    {
      if (serialPort.IsOpen)
      {
        try
        {
          sendStopCommand();
        }
        catch (IOException)
        {
        }
        try
        {
          Thread.Sleep(100);
          serialPort.Close();
        }
        catch (IOException)
        {
        }
      }
    }

    private static HashSet<string> LoaddeviceNames()
    {
      vorzeDeviceNames = new HashSet<string>();
      List<string> devices = new List<string>();
      devices.Add("Vorze_USB");
      devices.Add("A10 Cyclone2");

      foreach (string str in devices)
      {
        vorzeDeviceNames.Add(str);
      }
      return vorzeDeviceNames;
    }

    private static UsbDongle discoverUsbDongle()
    {
      List<UsbDongle> list = UsbDongle.discover().FindAll(d => vorzeDeviceNames.Contains(d.deviceName));
      if (0 >= list.Count)
      {
        return null;
      }
      return list[0];
    }

    private static void InitialCommand()
    {
      dataBuf[0] = 1;
      dataBuf[1] = 1;
      dataBuf[2] = 0;
    }

    private static void openComPort()
    {
      if (serialPort.IsOpen)
      {
        closeComPort();
      }
      if (!serialPort.IsOpen && (vorzeDongle != null))
      {
        serialPort.PortName = vorzeDongle.portName;
        serialPort.Open();
      }
    }

    private static void pausePlayer()
    {
      //timer.Stop();
      Thread.Sleep(100);
      try
      {
        sendStopCommand();
      }
      catch (Exception)
      {
      }
    }

    private static void resetCommand()
    {
      vorzeCounter = (uint)(currentPlayPositionMovie);
      vorzeNext = 0;
      comFileData.time = 0;
      while (setCommand())
      {
        if (vorzeCounter < comFileData.time)
        {
          return;
        }
        vorzeNext++;
      }
    }

    private static void resetComPort()
    {
      lock (syncObject)
      {
        if (serialPort.IsOpen)
        {
          try
          {
            Thread.Sleep(100);
            serialPort.Close();
            vorzeDongle = null;
          }
          catch (IOException)
          {
          }
        }
        if (discoverUsbDongle() != null)
        {
          openComPort();
        }
      }
    }

    private static void sendCommand(byte data)
    {
      try
      {
        if (serialPort.IsOpen)
        {
          DateTime now = DateTime.Now;
          TimeSpan span = now.Subtract(lastCmdSend);
          Debug.WriteLine("interval {0} miliseconds", span.TotalMilliseconds);
          if (span.TotalMilliseconds < 50.0)
          {
            Thread.Sleep((int)(50 - ((int)span.TotalMilliseconds)));
          }

          lastCmdSend = now;
          dataBuf[2] = data;
          serialPort.Write(dataBuf, 0, 3);
          Debug.WriteLine("serialPort.Write time={0}, data={1} done!!", comFileData.time, data);
        }
      }
      catch (Exception exception)
      {
        Debug.WriteLine("serialPort exception: {0}", exception.Message);
        if (exception is TimeoutException)
        {
          try
          {
            resetComPort();
          }
          catch (IOException)
          {
          }
        }
      }
    }

    private static void sendStopCommand()
    {
      sendCommand(0);
    }

    private static bool setCommand()
    {
      bool flag = false;
      if (vorzeNext < csvfileData.Count)
      {
        uint time = comFileData.time;
        string[] strArray = csvfileData[vorzeNext].ToString().Split(new char[] { ',' });
        try
        {
          comFileData.time = uint.Parse(strArray[0]);
          comFileData.data = (byte)((int.Parse(strArray[1]) * 0x80) + int.Parse(strArray[2]));
          flag = true;
        }
        catch (ArgumentNullException)
        {
          Debug.WriteLine("invalid data time=[{0}], data_type=[{1}], speed=[{2}]", strArray[0], strArray[1], strArray[2]);
          vorzeNext++;
          return setCommand();
        }
        catch (FormatException)
        {
          Debug.WriteLine("invalid data time=[{0}], data_type=[{1}], speed=[{2}]", strArray[0], strArray[1], strArray[2]);
          vorzeNext++;
          return setCommand();
        }
        catch (OverflowException)
        {
          Debug.WriteLine("invalid data time=[{0}], data_type=[{1}], speed=[{2}]", strArray[0], strArray[1], strArray[2]);
          vorzeNext++;
          return setCommand();
        }
        if (comFileData.time >= time)
        {
          return flag;
        }
        Debug.WriteLine("beck to time !! {0} -> {1}", time, comFileData.time);
        vorzeNext++;
        return setCommand();
      }
      comFileData.time = uint.MaxValue;
      return flag;
    }

    private static void timer_Tick(object sender, EventArgs e)
    {
      try
      {
        if (string.IsNullOrEmpty(currentlyActiveCSV) == false)
        {
          if (playerInactive)
          {
            //logger("Player is inactive", false);
            if (!playerPauseEnabled)
            {
              pausePlayer();
              playerPauseEnabled = true;
            }

            return;
          }

          if (playerWasInactive && !playerInactive)
          {
            logger("Resuming Vorze device after Zoom player was paused / stoped", true);
            playerWasInactive = false;
            playerPauseEnabled = false;
            restartPlayer();
            return;
          }

          if (changeCOMDevice)
          {
            updateUsbDongle();
          }

          vorzeCounter = (uint) (currentPlayPositionMovie);
          //Debug.WriteLine(vorzeCounter + " / " + vorzePre_counter);

          if (vorzePre_counter <= vorzeCounter)
          {
            // Fast forward or rewind detection, best way there is at the moment as we can't poll that data fast enough out of Zoom Player to detect it normally
            int TimeDif;
            try
            {
              if (vorzeCounter > 100 && comFileData.time > 100)
              {
                //Compare current counter with ComFile Time
                TimeDif = Math.Abs(int.Parse(vorzeCounter.ToString()) - int.Parse(comFileData.time.ToString()));

                if (TimeDif > 100)
                {
                  Debug.WriteLine("COM time dif too high: " + TimeDif);
                  resetCommand();
                }
                else
                {
                  // Compare ComFile Time with Counter
                  TimeDif = Math.Abs(int.Parse(comFileData.time.ToString()) - int.Parse(vorzeCounter.ToString()));
                  if (TimeDif > 100)
                  {
                    Debug.WriteLine("COM time dif too high: " + TimeDif);
                    resetCommand();
                  }
                }
              }
            }
            catch (Exception)
            {
            }

            //Debug.WriteLine("ComTime: " + comFileData.time);
            if (vorzeCounter > comFileData.time)
            {
              int counterDif = (int) (vorzeCounter - vorzePre_counter);
              //Debug.WriteLine("Counter dif: " + counterDif);
              if (counterDif >= 12)
              {
                resetCommand();
                //Thread.Sleep(1);
              }

              sendCommand(comFileData.data);
              vorzeNext++;
              setCommand();
            }
            else
            {
              vorzeCounter = (uint) (currentPlayPositionMovie);
            }
          }
          else
          {
            Debug.WriteLine("No new data - sending reset command..");

            resetCommand();
            sendStopCommand();
          }
          vorzePre_counter = vorzeCounter;
        }
      }
      catch (Exception et)
      {
        Debug.WriteLine("Error occured in timer");
        Debug.WriteLine(et.Message);
      }
    }

    private static void updateUsbDongle()
    {
      Debug.WriteLine("Updating usb Vorze Dongle...");

      lock (syncObject)
      {
        if (changeCOMDevice)
        {
          changeCOMDevice = false;
          UsbDongle dongle = null;
          dongle = discoverUsbDongle();
          if (dongle != null)
          {
            if (dongle == null)
            {
              dongle = dongle;
              openComPort();
              sendStopCommand();
              if (0 < vorzeCounter)
              {
                resetCommand();
              }
            }
          }
          else
          {
            dongle = null;
            try
            {
              Thread.Sleep(100);
              serialPort.Close();
            }
            catch (IOException)
            {
            }
          }
        }
      }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct ComType
    {
      public uint time;
      public byte data;
    }
  }
}

