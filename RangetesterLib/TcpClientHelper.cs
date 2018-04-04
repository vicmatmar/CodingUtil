using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace RangeTester
{
    class TcpClientHelper : TcpClient
    {
        int _read_to = 250;

        public int Read_TimeOut { get => _read_to; set => _read_to = value; }

        public string[] ReadAllLines()
        {
            var clientStream = GetStream();
            var clientReader = new StreamReader(clientStream);
            clientStream.ReadTimeout = Read_TimeOut;

            List<string> lines = new List<string>();
            while (!clientReader.EndOfStream)
            {
                try
                {
                    lines.Add(clientReader.ReadLine());
                }
                catch
                {
                    break;
                }

            }

            return lines.ToArray();
        }
    }
}