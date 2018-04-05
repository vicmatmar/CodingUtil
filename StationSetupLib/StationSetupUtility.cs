using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;

namespace CodingUtil
{
    public class StationSetupUtility
    {
        public static string GetMacAddress()
        {
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces().Where(nic => nic.OperationalStatus == OperationalStatus.Up);

            if (networkInterfaces.Any(nic => nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet))
            {
                return networkInterfaces.First(nic => nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet).GetPhysicalAddress().ToString();
            }
            else if (networkInterfaces.Any(nic => nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211))
            {
                return networkInterfaces.First(nic => nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211).GetPhysicalAddress().ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetFullPath(string exeName)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = "where";
                p.StartInfo.Arguments = exeName;
                p.StartInfo.RedirectStandardOutput = true;
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();

                if (p.ExitCode != 0)
                    return null;

                // just return first match
                return output.Substring(0, output.IndexOf(Environment.NewLine));
            }
            catch (Win32Exception)
            {
                throw new Exception("'where' command is not on path");
            }
        }

    }
}
