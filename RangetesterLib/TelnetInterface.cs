using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace RangeTester
{
    public class TelnetConnection : IDisposable
    {
        static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        TcpClient _tcpSocket;
        int _timeOutMs = 200;

        string _hostname;
        public string HostName { get { return _hostname; } }

        int _port;
        public int Port { get { return _port; } }

        /// <summary>
        /// Initializes a new instance of the System.Net.Sockets.TcpClient class and 
        /// connects to the specified port on the specified host.
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        public TelnetConnection(string hostname, int port)
        {
            _hostname = hostname;
            _port = port;
            _tcpSocket = new TcpClient(hostname, port);
        }

        public int Available
        {
            get { return _tcpSocket.Available; }
        }

        public void Dispose()
        {
            if (_tcpSocket != null)
                _tcpSocket.Close();
        }

        public void Close()
        {
            _tcpSocket.Close();
        }

        public void WriteLine(string cmd)
        {
            Write(cmd + "\n");
        }

        public void Write(string cmd)
        {
            _logger.Trace($"Write:{cmd}");

            if (!_tcpSocket.Connected) return;
            //byte[] buf = ASCIIEncoding.ASCII.GetBytes(cmd.Replace("\0xFF", "\0xFF\0xFF"));
            byte[] buf = ASCIIEncoding.ASCII.GetBytes(cmd);
            _tcpSocket.GetStream().Write(buf, 0, buf.Length);
        }

        public string Read()
        {
            string data = "";
            if (!_tcpSocket.Connected) return null;
            do
            {
                data += read();
                System.Threading.Thread.Sleep(_timeOutMs);
            } while (_tcpSocket.Available > 0);

            _logger.Trace($"Read:{data}");

            return data;
        }

        public bool IsConnected
        {
            get { return _tcpSocket.Connected; }
        }

        string read()
        {
            string data = "";
            while (_tcpSocket.Available > 0)
            {
                int input = _tcpSocket.GetStream().ReadByte();
                data += ((char)input);
            }
            return data;
        }
    }
}
