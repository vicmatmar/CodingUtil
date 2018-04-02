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
    }
}
