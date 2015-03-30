using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Vorze_PlayerHelper
{
  public partial class Form1 : Form
  {
    // UI DELEGATE
    private delegate void UniversalVoidDelegate();

    public Form1()
    {
      InitializeComponent();

      // Start log monitoring and cleanup old log file
      if (File.Exists("debug.log"))
      {
        File.Delete("debug.log");
      }

      Thread t = new Thread(logger);
      t.IsBackground = true;
      t.Start();

      // Init Vorze hardware
      VorzeHelper.Init();
    }

    private void saveSettings()
    {
      Properties.Settings.Default.Save();
    }

    private void tbZplayerIP_Validating(object sender, CancelEventArgs e)
    {
      saveSettings();
    }

    private void tbZplayerPort_Validating(object sender, CancelEventArgs e)
    {
      saveSettings();
    }

    private void tbVorzeDir_Validating(object sender, CancelEventArgs e)
    {
      saveSettings();
    }

    private void btnBrowseVorzeDir_Click(object sender, EventArgs e)
    {
      folderBrowserDialog1.SelectedPath = Properties.Settings.Default.VorzeRootDIR;
      if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
      {
        tbVorzeDir.Text = folderBrowserDialog1.SelectedPath;
      }    

      saveSettings();
    }

    private void cbMonitoringEnabled_CheckedChanged(object sender, EventArgs e)
    {
      if (cbMonitoringEnabled.Checked)
      {
        VorzeHelper.vorzeIsEnabled = true;
        VorzeHelper.startPlayer();
      }
      else
      {
        VorzeHelper.vorzeIsEnabled = false;
        VorzeHelper.stopPlayer();
      }
      saveSettings();
    }
    private void logger()
    {
      while (true)
      {
        try
        {
          if (File.Exists("debug.log"))
          {
            ControlInvike(lvLog, () => lvLog.Items.Clear());
            
            StreamReader sr = new StreamReader("debug.log");
            string line = "";

            while ((line = sr.ReadLine()) != null)
            {
              string datePrefix = DateTime.Now.ToString();
              string logText = string.Format("[ {0} ] {1}", datePrefix, line);
              ControlInvike(lvLog, () => lvLog.Items.Add(logText));
            }
            sr.Close();
          }
          ListviewScrollToBottom(lvLog);
          Thread.Sleep(5000);
        }
        catch (Exception)
        {
        }
      }
    }
    public void ListviewScrollToBottom(ListView lv)
    {
      try
      {
        ControlInvike(lv, () => lv.Items[lv.Items.Count - 1].EnsureVisible());
      }
      catch (Exception et)
      {
      }
    }

    public static void ControlInvike(Control control, Action function)
    {
      if (control.IsDisposed || control.Disposing)
        return;

      if (control.InvokeRequired)
      {
        control.Invoke(new UniversalVoidDelegate(() => ControlInvike(control, function)));
        return;
      }
      function();
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      try
      {
        VorzeHelper.stopPlayer();
        saveSettings();
      }
      catch (Exception)
      {
      }
    }
    private void cbMinimizeToTray_CheckedChanged(object sender, EventArgs e)
    {
      saveSettings();
    }

    private void notifyIcon1_Click(object sender, EventArgs e)
    {
      notifyIcon1.Visible = false;
      Show();
      WindowState = FormWindowState.Normal;
    }

    private void Form1_Resize(object sender, EventArgs e)
    {
      if (FormWindowState.Minimized == WindowState && Properties.Settings.Default.minimizeToTray)
      {
        notifyIcon1.Visible = true;
        Hide();
      }
    }
  }
}
