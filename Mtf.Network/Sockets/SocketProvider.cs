using System.Net;
using System.Net.Sockets;
using Mtf.Network.Port;

namespace Mtf.Network.Sockets
{
    public class SocketProvider
    {
        public const int MaxPendingConnections = 10; // Maximum number of pending connections
        public const int MaxTriesToGetSocket = 10; // Maximum number of attepts to get a socket
        public const int SocketConnectionTimeout = 3000;

        private readonly FreePortProvider freePortProvider;

        public SocketProvider()
        {
            freePortProvider = new FreePortProvider();
        }

        /// <summary>
        /// Get a TCP listener socket.
        /// </summary>
        /// <param name="ip">IP address to listen on.</param>
        /// <param name="port">Port to listen on. Pass 0 to create a random port to listen.</param>
        /// <returns>The listener socker.</returns>
        public Socket GetListenerSocket(IPAddress ip, int port = 0)
        {
            return GetSocket(ip, port, AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp, true);
        }

        /// <summary>
        /// Get a TCP client socket to send data.
        /// </summary>
        /// <param name="ip">IP address to connect.</param>
        /// <param name="port">Port to connect. Pass 0 to connect to a random port.</param>
        /// <returns>The sender socket.</returns>
        public Socket GetClientSocket(IPAddress ip, int port = 0)
        {
            return GetSocket(ip, port, AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp, false);
        }

        private Socket GetSocket(IPAddress ip, int port, AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType, bool server)
        {
            Socket socket = null;
            SocketException rex = null;
            bool error;
            var tries = 0;
            do
            {
                tries++;
                try
                {
                    socket = new Socket(addressFamily, socketType, protocolType);

                    if (port == 0)
                    {
                        port = freePortProvider.GetFreePort();
                    }

                    bool success;
                    if (server)
                    {
                        socket.Bind(new IPEndPoint(ip, port));
                        socket.Listen(MaxPendingConnections);
                        success = true;
                    }
                    else
                    {
                        var result = socket.BeginConnect(ip, port, null, null);
                        success = result.AsyncWaitHandle.WaitOne(SocketConnectionTimeout, true);
                    }

                    error = !success;
                }
                catch (SocketException ex)
                {
                    rex = ex;
                    error = true;
                }
            }
            while (error && tries < MaxTriesToGetSocket);

            if (error && tries == MaxTriesToGetSocket)
            {
                if (rex != null)
                {
                    throw rex;
                }
            }
            return socket;
        }
    }
}