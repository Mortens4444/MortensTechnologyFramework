using System;
using System.Net;

namespace Mtf.Network
{
    public class InetAddrConverter
    {
        // 1.2.3.4 . 0x04030201
        public uint Convert(string ip)
        {
            var ipParts = ip.Split('.');
            if (ipParts.Length != 4)
            {
                throw new ArgumentException("IPv4 address format is incorrect", nameof(ip));
            }
            var ipPart1 = System.Convert.ToByte(ipParts[0]);
            var ipPart2 = System.Convert.ToByte(ipParts[1]);
            var ipPart3 = System.Convert.ToByte(ipParts[2]);
            var ipPart4 = System.Convert.ToByte(ipParts[3]);
            var ipAddr = (uint)(ipPart1 + ipPart2 * 256 + ipPart3 * 256 * 256 + ipPart4 * 256 * 256 * 256);
            return ipAddr;
        }

        public uint Convert(IPAddress ip)
        {
            return BitConverter.ToUInt32(ip.GetAddressBytes(), 0);
        }
    }
}