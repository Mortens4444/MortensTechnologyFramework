using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace Mtf.Network.PhysicalAddress
{
    public class MacAddressProvider
    {
        private readonly MacAddressConverter macAddressConverter = new MacAddressConverter();

        public string[] GetMacAddresses()
        {
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            return networkInterfaces.Select(
                networkInterface => macAddressConverter.MAC_PhysicalAddressToString(
                    networkInterface.GetPhysicalAddress())).ToArray();
        }

        public string GetMACAddress(string ipOrHost)
        {
            if (String.IsNullOrEmpty(ipOrHost)) return String.Empty;
            var ipAddress = IPAddress.Parse(ipOrHost);
            var mac = macAddressConverter.IPAddressToMACAddress(ipAddress);
            return macAddressConverter.MAC_PhysicalAddressToString(mac);
        }
    }
}