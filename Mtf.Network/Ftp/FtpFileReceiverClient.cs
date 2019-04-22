using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Mtf.Network.Host;

namespace Mtf.Network.Ftp
{
    public class FtpFileReceiverClient
    {
        public const int NotFound = -1;
        public const string Localhost = "localhost";
        public const string _127_0_0_1 = "127.0.0.1";
        public const int MaxBufferSize = 8192;

        private readonly IpUtils ipUtils;

        private readonly string path;

        /// <summary>
        /// Creates an FtpFileReceiverClient.
        /// </summary>
        /// <param name="path">Path to save files. Eg.: "D:/"</param>
        public FtpFileReceiverClient(string path)
        {
            ipUtils = new IpUtils();
            this.path = path;
        }

        public bool Send(string host, int listenerPortOfServer, string[] commands, string addressFamilyName, string socketTypeName, string protocolTypeName, int timeoutInMilliseconds)
        {
            return Send(host, listenerPortOfServer, commands, GetAddressFamily(addressFamilyName), GetSocketType(socketTypeName), GetProtocolType(protocolTypeName), timeoutInMilliseconds);
        }

        public bool Send(string host, int listenerPortOfServer, string[] commands, AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType, int timeoutInMilliseconds)
        {
            var result = false;

            Socket socket = null;
            TcpListener dataSocket = null;
            try
            {
                var ipAddress = GetIpAddress();
                if (ipAddress == null)
                {
                    return false;
                }

                dataSocket = new TcpListener(IPAddress.Parse(ipAddress), 0);
                dataSocket.Start();

                var ip = ((IPEndPoint)dataSocket.LocalEndpoint).Address;

                socket = new Socket(addressFamily, socketType, protocolType)
                {
                    SendBufferSize = MaxBufferSize,
                    SendTimeout = timeoutInMilliseconds,
                    ReceiveTimeout = timeoutInMilliseconds
                    //ReceiveBufferSize = MaxBufferSize
                };

                var res = socket.BeginConnect(host, listenerPortOfServer, null, null);
                var success = res.AsyncWaitHandle.WaitOne(timeoutInMilliseconds, true);

                if (success)
                {
                    for (var i = 0; i < commands.Length; i++)
                    {
                        if (commands[i] == "PORT ")
                        {
                            var ipStr = ipUtils.A1A2A3A4P1P2(ip.ToString(), ((IPEndPoint)dataSocket.LocalEndpoint).Port);
                            commands[i] += String.Concat(ipStr, "\r\n");
                        }

                        var sendBuffer = Encoding.ASCII.GetBytes(commands[i]);
                        socket.Send(sendBuffer, sendBuffer.Length, SocketFlags.None);

                        if (commands[i].IndexOf("RETR ", StringComparison.Ordinal) != 0)
                        {
                            WaitForSocketData(socket, 1000);
                        }

                        do
                        {
                            var receiveBuffer = new byte[socket.Available];
                            var readedBytes = socket.Receive(receiveBuffer, receiveBuffer.Length, SocketFlags.None);
                            if (readedBytes > 0)
                            {
                                var s = new String(Encoding.ASCII.GetChars(receiveBuffer, 0, readedBytes));
                                if (s.IndexOf("150 ") == 0 || s.IndexOf("125 ") == 0)
                                {
                                    while (!dataSocket.Pending())
                                    {
                                        Thread.Sleep(1);
                                    }

                                    var ds = dataSocket.AcceptSocket();
                                    WaitForSocketData(ds, Int32.MaxValue);

                                    while (true)
                                    {
                                        if (!WaitForSocketData(ds, 10000))
                                        {
                                            break;
                                        }

                                        var dataReceiveBuffer = new byte[ds.Available];
                                        /*var readBytes =*/ ds.Receive(dataReceiveBuffer, dataReceiveBuffer.Length, SocketFlags.None);

                                        if (commands[i].IndexOf("RETR ") == 0)
                                        {
                                            var fullPath = path + commands[i].Replace("RETR ", String.Empty).Replace("\r\n", String.Empty);
                                            using (var fileStream = File.Open(fullPath, FileMode.Append))
                                            {
                                                using (var binaryWriter = new BinaryWriter(fileStream))
                                                {
                                                    binaryWriter.Write(dataReceiveBuffer);
                                                    binaryWriter.Flush();
                                                    binaryWriter.Close();
                                                }
                                                fileStream.Close();
                                            }
                                        }
                                    }
                                }
                            }

                            WaitForSocketData(socket, 100);
                        }
                        while (socket.Available > 0);
                        result = true;
                    }
                }
            }
            catch { }
            finally
            {
                try
                {
                    dataSocket?.Stop();
                    if (socket != null)
                    {
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                    }
                }
                catch { }
            }

            return result;
        }

        private string GetIpAddress()
        {
            var ipAddresses = ipUtils.GetIpAddresses();
            return ipAddresses.FirstOrDefault(ipAddress => ipAddress != _127_0_0_1 && ipAddress != Localhost && ipAddress.IndexOf(".", StringComparison.Ordinal) == NotFound);
        }

        private static bool WaitForSocketData(Socket socket, int waitTime)
        {
            var spentTime = 0;
            while (socket.Available == 0 && spentTime < waitTime)
            {
                Thread.Sleep(1);
                spentTime++;
            }
            return spentTime != waitTime || socket.Available != 0;
        }

        private static AddressFamily GetAddressFamily(string addressFamilyName)
        {
            return (AddressFamily)Enum.Parse(typeof(AddressFamily), addressFamilyName);
        }

        private static SocketType GetSocketType(string socketTypeName)
        {
            return (SocketType)Enum.Parse(typeof(SocketType), socketTypeName);
        }

        private static ProtocolType GetProtocolType(string protocolTypeName)
        {
            return (ProtocolType)Enum.Parse(typeof(ProtocolType), protocolTypeName);
        }
    }
}