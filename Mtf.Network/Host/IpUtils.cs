using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Mtf.Utils.ByteExtensions;

namespace Mtf.Network.Host
{
    public class IpUtils
    {
        public IEnumerable<string> GetIpAddresses()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return from ipAddress in host.AddressList
                where ipAddress.AddressFamily == AddressFamily.InterNetwork
                select ipAddress.ToString();
        }

        public IpClass GetIP_Class(string ipAddress)
        {
            var ipParts = GetIpAddressBytes(ipAddress);
            if (ipParts.Length != 4)
            {
                throw new ArgumentException("Parameter format is not correct", nameof(ipAddress));
            }

            if (ipParts[0].IsBetweenInclusive(0, 126))
            {
                return IpClass.ClassA;
            }
            if (ipParts[0].IsBetweenInclusive(128, 191))
            {
                return IpClass.ClassB;
            }
            if (ipParts[0].IsBetweenInclusive(192, 223))
            {
                return IpClass.ClassC;
            }
            return ipParts[0].IsBetweenInclusive(224, 239) ? IpClass.ClassD : IpClass.ClassE;
        }

        public string A1A2A3A4P1P2(string ipAddress, int portNumber)
        {
            var port = new Port.Port(portNumber);
            return $"{ipAddress.Replace('.', ',')},{port}";
        }

        public bool IsIPv4Address(string ipAddress)
        {
            try
            {
                var ipParts = GetIpAddressBytes(ipAddress);
                if (ipParts.Length == 4)
                {
                    if (ipParts[0].IsBetweenInclusive(0, 255))
                    {
                        if (ipParts[1].IsBetweenInclusive(0, 255))
                        {
                            if (ipParts[2].IsBetweenInclusive(0, 255))
                            {
                                if (ipParts[3].IsBetweenInclusive(0, 255))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            return false;
        }

        public bool IsLocalIpAddress(string host)
        {
            try
            {
                var hostIpArray = Dns.GetHostAddresses(host);
                var localIpArray = Dns.GetHostAddresses(Dns.GetHostName());

                foreach (var ip in hostIpArray)
                {
                    if (IPAddress.IsLoopback(ip)) return true;
                    foreach (var localIp in localIpArray)
                    {
                        if (ip.Equals(localIp)) return true;
                    }
                }
            }
            catch { }
            return false;
        }

        private static byte[] GetIpAddressBytes(string ipAddress)
        {
            var parts = ipAddress.Split('.');
            if (parts.Length == 4)
            {
                return new[]
                {
                    Convert.ToByte(parts[0]), Convert.ToByte(parts[1]),
                    Convert.ToByte(parts[2]), Convert.ToByte(parts[3])
                };
            }
            return new byte[] {};
        }
    }
}