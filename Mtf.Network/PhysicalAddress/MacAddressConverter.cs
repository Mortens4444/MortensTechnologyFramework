using System;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Mtf.Linux;

namespace Mtf.Network.PhysicalAddress
{
    public class MacAddressConverter
    {
        [DllImport("Iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(uint DestIP, uint SrcIP, byte[] pMacAddr, ref uint PhyAddrLen);

        public const int MacAddressLengthInBytes = 6;

        public System.Net.NetworkInformation.PhysicalAddress MAC_StringToPhysicalAddress(string macString)
        {
            return MAC_ByteArrayToPhysicalAddress(MAC_StringToByteArray(macString));
        }

        public byte[] MAC_StringToByteArray(string macString)
        {
            if (macString == null)
            {
                throw new ArgumentNullException(nameof(macString), "Parameter is null");
            }
            var mac = Regex.Replace(macString, "[^0-9A-Fa-f]", String.Empty);
            if (mac.Length != MacAddressLengthInBytes * 2)
            {
                throw new ArgumentException("Incorrect MAC address", nameof(macString));
            }

            var result = new byte[MacAddressLengthInBytes];
            for (var i = 0; i < result.Length; i++)
            {
                var hexa = new string(new[] { mac[i * 2], mac[i * 2 + 1] });
                result[i] = Byte.Parse(hexa, NumberStyles.HexNumber);
            }
            return result;
        }

        public System.Net.NetworkInformation.PhysicalAddress MAC_ByteArrayToPhysicalAddress(byte[] macAddress)
        {
            return new System.Net.NetworkInformation.PhysicalAddress(macAddress);
        }

        public string MAC_PhysicalAddressToString(System.Net.NetworkInformation.PhysicalAddress mac)
        {
            return MAC_PhysicalAddressToString(mac, ':');
        }

        public string MAC_PhysicalAddressToString(System.Net.NetworkInformation.PhysicalAddress mac, char? separator)
        {
            var macString = mac.ToString();
            if (separator == null)
            {
                return macString;
            }
            var friendlyMac = new StringBuilder();
            for (var i = 0; i < macString.Length; i += 2)
            {
                if (i > 0)
                {
                    friendlyMac.Append(separator);
                }
                friendlyMac.Append(macString.Substring(i, 2).ToUpper());
            }

            return friendlyMac.ToString();
        }

        public System.Net.NetworkInformation.PhysicalAddress IPAddressToMACAddress(IPAddress ip)
        {
            //check what family the ip is from <cref="http://msdn.microsoft.com/en-us/library/system.net.sockets.addressfamily.aspx"/>
            if (ip.AddressFamily != AddressFamily.InterNetwork)
                throw new ArgumentException("The remote system only supports IPv4 addresses");

            var macByteArray = new byte[MacAddressLengthInBytes]; // 48 bit

#if WINDOWS
            var inetAddrConverter = new InetAddrConverter();
            var receiverIp = inetAddrConverter.Convert(ip);
            const uint senderIp = 0; //inetAddrConverter.Convert(new IpAddressProvider().HostNameToIPAddress(String.Empty));
            var length = (uint)macByteArray.Length;

            //call the Win32 API SendArp <cref="http://msdn.microsoft.com/en-us/library/aa366358%28VS.85%29.aspx"/>
            var arpReply = SendARP(receiverIp, senderIp, macByteArray, ref length);
            if ((SystemErrorCodes)arpReply != SystemErrorCodes.SUCCESS)
            {
                switch ((SystemErrorCodes)arpReply)
                {
                    case SystemErrorCodes.ERROR_GEN_FAILURE:
                        throw new Exception("A device attached to the system is not functioning. This error is returned on Windows Server 2003 and earlier when an ARP reply to the SendARP request was not received. This error can occur if destination IPv4 address could not be reached because it is not on the same subnet or the destination computer is not operating.");
                    case SystemErrorCodes.ERROR_INVALID_PARAMETER:
                        throw new Exception("One of the parameters is invalid. This error is returned on Windows Server 2003 and earlier if either the pMacAddr or PhyAddrLen parameter is a NULL pointer.");
                    case SystemErrorCodes.ERROR_INVALID_USER_BUFFER:
                        throw new Exception("The supplied user buffer is not valid for the requested operation. This error is returned on Windows Server 2003 and earlier if the ULONG value pointed to by the PhyAddrLen parameter is zero.");
                    case SystemErrorCodes.ERROR_BAD_NET_NAME:
                        throw new Exception("The network name cannot be found. This error is returned on Windows Vista and later when an ARP reply to the SendARP request was not received. This error occurs if the destination IPv4 address could not be reached.");
                    case SystemErrorCodes.ERROR_BUFFER_OVERFLOW:
                        throw new Exception("The file name is too long. This error is returned on Windows Vista if the ULONG value pointed to by the PhyAddrLen parameter is less than 6, the size required to store a complete physical address.");
                    case SystemErrorCodes.ERROR_NOT_FOUND:
                        throw new Exception("Element not found. This error is returned on Windows Vista if the the SrcIp parameter does not specify a source IPv4 address on an interface on the local computer or the INADDR_ANY IP address (an IPv4 address of 0.0.0.0).");
                    default:
                        throw new Win32Exception(arpReply);
                }
            }
#else
            // sudo
            var arpCommand = $"nmap -sP -PE -PA {ip} | grep MAC | awk '{{ print $3; }}'";
            var shellScriptExecutor = new ShellScriptExecutor();
            var arpResult = shellScriptExecutor.GetCommandResult(arpCommand);

            var macAddress = arpResult.HasSucceeded ? arpResult.Output : "00-00-00-00-00-00";
            var macAddressConverter = new MacAddressConverter();
            macByteArray = macAddressConverter.MAC_StringToByteArray(macAddress);
#endif

            //return the MAC address in a PhysicalAddress format
            return new System.Net.NetworkInformation.PhysicalAddress(macByteArray);
        }
    }
}