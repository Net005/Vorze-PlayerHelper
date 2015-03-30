namespace Cyclone2.Devices
{
    using System;
    using System.Collections.Generic;
    using System.Management;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    internal class UsbDongle
    {
        private static readonly Regex deviceNameRegex = new Regex(@" \((COM[1-9][0-9]?[0-9]?)\)$");

        public UsbDongle(string fullname)
        {
            Match match = deviceNameRegex.Match(fullname);
            this.fullname = fullname;
            this.portName = match.Groups[1].Value;
            this.deviceName = fullname.Replace(match.Groups[0].Value, "");
        }

        public static List<UsbDongle> discover()
        {
            List<UsbDongle> list = new List<UsbDongle>();
            ManagementClass class2 = new ManagementClass("Win32_PnPEntity");
            foreach (ManagementObject obj2 in class2.GetInstances())
            {
                object propertyValue = obj2.GetPropertyValue("Name");
                if (propertyValue != null)
                {
                    string input = propertyValue.ToString();
                    if (deviceNameRegex.IsMatch(input))
                    {
                        Console.WriteLine(input);
                        list.Add(new UsbDongle(input));
                    }
                }
            }
            return list;
        }

        public string deviceName { get; protected set; }

        public string fullname { get; protected set; }

        public string portName { get; protected set; }
    }
}

