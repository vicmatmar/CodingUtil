using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;

namespace RangeTester
{
    public enum PA_ENABLED_SETTING : int { OFF = 0, ON = 1, DEFAULT }
    public enum DEVICE_TYPES : int { EM3X = 0, EFR32 }

    public interface IRangeTester
    {
        RtPingResults Ping(int ping_count);

        string Client_Host { get; set; }
        int Client_Port { get; set; }

        int Channel { get; set; }

        string Server_Host { get; set; }

        int Server_Port { get; set; }

        void Server_Init();

        int Alt_Cfg { get; set; }

        int Power { get; set; }

        PA_ENABLED_SETTING PAEnabled { get; set; }

    }
}
