namespace Cyclone2.windows
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    internal class ProcessUtil
    {
        private const int SW_NORMAL = 1;

        [DllImport("USER32.DLL", CharSet=CharSet.Auto)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        public static bool ShowPrevProcess()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] processesByName = Process.GetProcessesByName(currentProcess.ProcessName);
            int id = currentProcess.Id;
            foreach (Process process2 in processesByName)
            {
                if (process2.Id != id)
                {
                    ShowWindow(process2.MainWindowHandle, 1);
                    SetForegroundWindow(process2.MainWindowHandle);
                    return true;
                }
            }
            return false;
        }

        [DllImport("USER32.DLL", CharSet=CharSet.Auto)]
        private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}

