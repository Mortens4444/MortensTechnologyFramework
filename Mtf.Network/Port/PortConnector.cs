using System.Net;
using System.Net.Sockets;

namespace Mtf.Network.Port
{
    public class PortConnector
    {
        public const int SocketConnectionTimeout = 3000;

        public Socket Socket { get; }

        public string HostnameOrIp { get; }

        public int PortNumber { get; }

        public PortConnector(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType, string hostnameOrIp, int portNumber)
        {
            HostnameOrIp = hostnameOrIp;
            PortNumber = portNumber;
            Socket = new Socket(addressFamily, socketType, protocolType)
            {
                ReceiveTimeout = SocketConnectionTimeout,
                SendTimeout = SocketConnectionTimeout
            };
        }

        public PortConnector(Socket socket)
        {
            var endpoint = (IPEndPoint)socket.RemoteEndPoint;
            HostnameOrIp = endpoint.Address.ToString();
            PortNumber = endpoint.Port;
            Socket = socket;
        }
    }
}
