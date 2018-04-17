using System;
using System.Collections.Generic;
using System.Linq;

using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace RangeTester
{
    public class EFR32xRT : IRangeTester
    {
        const string _PING_REGEX = @"(PING (\d*) (-\d*) (\d*) (-\d*))";

        const int _LEGACY_RTM_PORT_NUMBER = 10001;

        int _server_port = 4900;
        public int Server_Port { get { return _server_port; } set { _server_port = value; } }

        string _server_host;
        public string Server_Host { get { return _server_host; } set { _server_host = value; } }

        string _client_host;
        public string Client_Host { get { return _client_host; } set { _client_host = value; } }

        int _client_port = 4901;
        public int Client_Port { get { return _client_port; } set { _client_port = value; } }

        int _channel = 17;
        public int Channel { get { return _channel; } set { _channel = value; } }

        int _power = int.MaxValue;
        public int Power { get => _power; set => _power = value; }

        PA_ENABLED_SETTING _pa_enabled = PA_ENABLED_SETTING.DEFAULT;
        public PA_ENABLED_SETTING PAEnabled { get => _pa_enabled; set => _pa_enabled = value; }

        int _atl_cfg = 0;
        public int Alt_Cfg { get { return _atl_cfg; } set { _atl_cfg = value; } }

        public RtPingResults Ping(int ping_count)
        {
            string response;

            // Init Client Connection
            TelnetConnection client = new TelnetConnection(Client_Host, Client_Port);

            // Start 
            string lines;
            int try_count = 0;
            while (true)
            {
                client.WriteLine("custom stop_network_scan");
                // Sleep while still scanning
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(500);
                    lines = client.Read();
                    if (!lines.Contains("SCANNING"))
                        break;
                }

                //clientWriter.WriteLine("plugin mfglib mfgenable 1");
                client.WriteLine("plugin mfglib stop");
                lines = client.Read();

                var clientFilter = (UInt32)((new Random((int)DateTime.Now.Ticks).NextDouble()) * UInt32.MaxValue);
                client.WriteLine($"custom rt-init {clientFilter} {Alt_Cfg}");  // Alt_Cfg does nothing for Highfin
                lines = client.Read();
                int count = 0;
                foreach (string line in lines.Split(new string[] { "\r\n" }, StringSplitOptions.None))
                    if (line == "STATUS 00")
                        count++;

                if (count == 2)
                    break;

                if(try_count++ > 3)
                    throw new Exception("Unable to init range test client");
            }


            client.WriteLine($"plugin mfglib set-channel {Channel}");
            response = client.Read();
            if (!response.Contains("status 0x00"))
            {
                throw new Exception("Unable to set client channel");
            }

            // This may be wrong 
            // for efr32 parameters are power level and power mode
            // for em3x radio tx power and Enable PA
            if (Power != int.MaxValue)
            {
                string cmd = "plugin mfglib set-power " + Power.ToString() + " 0";
                client.WriteLine(cmd);
                response = client.Read();
                if (!response.Contains("status 0x00"))
                {
                    throw new Exception("Unable to set client power");
                }
            }

            var txLqi = new List<int>();
            var txRssi = new List<int>();
            var rxLqi = new List<int>();
            var rxRssi = new List<int>();

            for (var i = 0; i < ping_count; i++)
            {
                int retry = 0;
                while (true)
                {
                    client.WriteLine("custom rt-ping");
                    Thread.Sleep(50);

                    response = client.Read();

                    if (Regex.IsMatch(response, _PING_REGEX))
                    {
                        var match = Regex.Match(response, _PING_REGEX);

                        txLqi.Add(int.Parse(match.Groups[2].Value));
                        txRssi.Add(int.Parse(match.Groups[3].Value));
                        rxLqi.Add(int.Parse(match.Groups[4].Value));
                        rxRssi.Add(int.Parse(match.Groups[5].Value));

                        break;
                    }
                    else
                    {
                        retry++;
                        if(retry > 5)
                            throw new Exception("Invalid Ping Response: " + response);
                    }

                }
            }

            client.WriteLine("plugin mfglib stop");
            client.WriteLine("plugin mfglib mfgenable 0");

            client.Close();

            RtPingResults results = new RtPingResults(txLqi.Average(), txRssi.Average(), rxLqi.Average(), rxRssi.Average());
            return results;

        }

        /// <summary>
        /// Initializes the RT server channel, etc
        /// 
        /// Note: The Digi X2/Lion fish combo seems to have special code
        /// to automate starting mfglib, etc.  The only command available is to
        /// set the channel.  So a special "legacy mode" was created to be able to use
        /// that setup.  The normal approach requires to ISA3 Adapters and server and client
        /// running the normal range test firmware.
        /// </summary>
        public void Server_Init()
        {
            string response = "";

            var rtm = new TcpClient();
            rtm.Connect(Server_Host, Server_Port);
            var rtmStream = rtm.GetStream();
            var rtmReader = new StreamReader(rtmStream);
            var rtmWriter = new StreamWriter(rtmStream);
            rtmWriter.AutoFlush = true;

            // Init RTM Connection
            if (Server_Port != _LEGACY_RTM_PORT_NUMBER)
            {
                // These command will generate "ERR No such command" for the legacy rtm.  
                rtmWriter.WriteLine("mfglib end");
                response = rtmReader.ReadLine();
                rtmWriter.WriteLine("mfglib start");
                rtmWriter.WriteLine("mfglib channel set {0}", Channel);
                response = rtmReader.ReadLine();
                rtmWriter.WriteLine("mfglib setPower 3 6");
                rtmWriter.WriteLine("mfglib rt-master 1");
            }
            else
            {
                // Do this mainly to flush out any bad data and the next write works!!!
                rtmWriter.WriteLine("mfglib channel set {0}", Channel);
            }

            // Verify we can talk to server
            // Note also this seems to be the only command available in legacy mode
            rtmWriter.WriteLine("mfglib channel set {0}", Channel);
            //Console.WriteLine("mfglib channel set {0}", Channel);

            response = rtmReader.ReadLine();
            if (response != "STATUS 00")
            {
                throw new Exception("Unable to set server channel");
            }

            rtm.Close();
        }
    }
}
