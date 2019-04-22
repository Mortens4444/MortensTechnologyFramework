using System;
using System.Net;
using System.Net.Sockets;

namespace Mtf.Network.PhysicalAddress
{
    /// <summary>
    /// Vékony lány
    /// </summary>
    public class WakeOnLan
    {
        public const int MacAddressLengthInBytes = 6;
        public const int MacAddressRepetitionsInMagicPacket = 16;

        private readonly MacAddressConverter macAddressConverter;

        public WakeOnLan()
        {
            macAddressConverter = new MacAddressConverter();
        }

        public byte[] CreateMagicPacket(byte[] macByteArray)
        {
            var result = new byte[MacAddressLengthInBytes + MacAddressRepetitionsInMagicPacket * MacAddressLengthInBytes];
            for (var i = 0; i < MacAddressLengthInBytes; i++)
            {
                result[i] = 255;
            }
            for (var i = 0; i < MacAddressRepetitionsInMagicPacket; i++)
            {
                for (var j = 0; j < MacAddressLengthInBytes; j++)
                {
                    result[i * MacAddressLengthInBytes + j + MacAddressLengthInBytes] = macByteArray[j];
                }
            }
            return result;
        }

        public void WakeOnLAN(System.Net.NetworkInformation.PhysicalAddress mac)
        {
            WakeOnLAN(macAddressConverter.MAC_PhysicalAddressToString(mac), 7);
        }

        /// <summary>
        /// Wake up a turned off computer over LAN.
        /// </summary>
        /// <param name="mac">MAC addres to send to magic packet.</param>
        /// <param name="port">7 and 9 are default ports for Wake on LAN</param>
        public void WakeOnLAN(System.Net.NetworkInformation.PhysicalAddress mac, ushort port)
        {
            WakeOnLAN(macAddressConverter.MAC_PhysicalAddressToString(mac), port);
        }

        public int WakeOnLAN(string macAddress)
        {
            return WakeOnLAN(macAddress, 7);
        }

        public int WakeOnLAN(string macAddress, ushort port)
        {
            int sentBytes;
            if (macAddress == null)
            {
                throw new ArgumentNullException(nameof(macAddress), "Parameter is null");
            }

            var macByteArray = macAddressConverter.MAC_StringToByteArray(macAddress);
            var magicPacket = CreateMagicPacket(macByteArray);
            var ep = new IPEndPoint(IPAddress.Broadcast, port);

            var clientSocket = new Socket(ep.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            try
            {
                clientSocket.Connect(ep);
                sentBytes = clientSocket.Send(magicPacket, 0, magicPacket.Length, SocketFlags.None);
                clientSocket.Close();
            }
            catch (SocketException)
            {
                if (clientSocket != null) clientSocket.Close();
                throw;
            }
            return sentBytes;
        }
    }
}