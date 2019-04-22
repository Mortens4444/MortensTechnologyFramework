using System.Linq;
using System.Net.NetworkInformation;

namespace Mtf.Network.Port
{
    public class PortExaminer
    {
        /// <summary>
        /// Checks if a port is available on the local machine.
        /// </summary>
        /// <param name="port">Number of the port to be checked.</param>
        /// <returns>True if the port is free.</returns>
        public bool IsLocalPortAvailable(int port)
        {
            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            var tcpConnections = ipGlobalProperties.GetActiveTcpConnections();
            return tcpConnections.All(tcpConnection => tcpConnection.LocalEndPoint.Port != port);
        }
    }
}