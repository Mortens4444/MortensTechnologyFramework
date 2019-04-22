using System.Net;
using System.Net.Sockets;

namespace Mtf.Network.Host
{
    public class IpAddressProvider
    {
        public IPAddress HostNameToIPAddress(string hostname)
        {
            var ipHost = Dns.GetHostEntry(hostname);
            var addresses = ipHost.AddressList;

            var i = 0;
            while (i < addresses.Length)
            {
                if (addresses[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    return addresses[i];
                }
                i++;
            }
            return null;
        }

        public string HostNameToIPAddressString(string hostname)
        {
            var result = HostNameToIPAddress(hostname);
            return result?.ToString();
        }
    }
}