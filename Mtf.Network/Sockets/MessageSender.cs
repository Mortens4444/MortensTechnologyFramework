using System;
using System.IO;
using System.Net.Sockets;

namespace Mtf.Network.Sockets
{
    public class MessageSender
    {
        public const int SocketConnectionTimeout = 3000;

        private readonly string ip;
        private readonly int serverListenerPort;

        public MessageSender(string ip, int serverListenerPort)
        {
            this.ip = ip;
            this.serverListenerPort = serverListenerPort;
        }

        public bool Send(string message)
        {
            bool result;
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                var res = socket.BeginConnect(ip, serverListenerPort, null, null);
                result = res.AsyncWaitHandle.WaitOne(SocketConnectionTimeout, true);

                if (result)
                {
                    using (var networkStream = new NetworkStream(socket))
                    {
                        using (var streamWriter = new StreamWriter(networkStream))
                        {
                            streamWriter.Write(message);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }
                        networkStream.Close();
                    }
                }
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            return result;
        }
    }
}