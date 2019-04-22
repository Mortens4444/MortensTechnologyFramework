using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Mtf.Network.Client;
using Mtf.Network.Packets.Snmp;
using Mtf.Utils.ByteArrayExtensions;
using Mtf.Utils.Int64Extensions;

namespace Mtf.Network.Snmp
{
    /// <summary>
    /// RFC 1988, RFC 1993, RFC 2002
    /// </summary>
    public class SnmpClient : ClientBase
    {
        public const string SystemInformation = "1.3.6.1.2.1.1.1.0";
        public const string Oids = "1.3.6.1.2.1.1.2.0";
        public const string Uptime = "1.3.6.1.2.1.1.3.0";
        public const string Sysadmin = "1.3.6.1.2.1.1.4.0";
        public const string RemoteHost = "1.3.6.1.2.1.1.5.0";
        public const string ServerRoom = "1.3.6.1.2.1.1.6.0";
        public const string Timeticks = "1.3.6.1.2.1.1.8.0";

        public const string ColdStart = "1.3.6.1.6.3.1.1.5.1";

        public const string A = "1.3.6.1.2.1.1.9.1.2.1";
        public const string B = "1.3.6.1.2.1.1.9.1.2.2";
        public const string C = "1.3.6.1.2.1.1.9.1.2.3";

        public SnmpClient(string serverHostnameOrIpAddress, DataArrivedEventHandler dataArrived)
            : base(serverHostnameOrIpAddress, dataArrived, (ushort)ClientType.SNMP)
        {
        }

        public SnmpMethod Method { get; set; } = SnmpMethod.Get;

        public string SnmpCommunity { get; set; }

        public void Send(string oid, SnmpMethod method)
        {
            Method = method;
            Send(oid);
        }

        public void GetSystemInformation()
        {
            Method = SnmpMethod.Get;
            Send(SystemInformation);
        }

        public void GetOIDs()
        {
            Method = SnmpMethod.Get;
            Send(Oids);
        }

        public void GetUptime()
        {
            Method = SnmpMethod.Get;
            Send(Uptime);
        }

        public void ColdStartAction()
        {
            Method = SnmpMethod.Get;
            Send(ColdStart);
        }

        public void GetSysAdmin()
        {
            Method = SnmpMethod.Get;
            Send(Sysadmin);
        }

        public void GetRemoteHost()
        {
            Method = SnmpMethod.Get;
            Send(RemoteHost);
        }

        public void GetServerRoom()
        {
            Method = SnmpMethod.Get;
            Send(ServerRoom);
        }

        public void GetTimeticks()
        {
            Method = SnmpMethod.Get;
            Send(Timeticks);
        }

        public void GetA()
        {
            Method = SnmpMethod.Get;
            Send(A);
        }

        public void GetB()
        {
            Method = SnmpMethod.Get;
            Send(B);
        }

        public void GetC()
        {
            Method = SnmpMethod.Get;
            Send(C);
        }

        /// <summary>
        /// Sends data, if socket is not connected, try to send to all clients.
        /// </summary>
        /// <param name="bytes">The byte array to send.</param>
        /// <returns>True, if all bytes has been sent</returns>
        public override bool Send(byte[] bytes)
        {
            var packet = new SnmpPacket(SnmpCommunity, bytes.ToAsciiString(), Method);
            var sentBytes = Socket.Send(packet.Payload, packet.Payload.Length, SocketFlags.None);
            return sentBytes == packet.Payload.Length;
        }

        private void Receiver()
        {
            while (Socket.Connected)
            {
                if (Socket.Available > 0)
                {
                    Thread.Sleep(200);
                    var readable = Socket.Available;

                    var receiveBuffer = new byte[readable];
                    var readBytes = Socket.Receive(receiveBuffer, receiveBuffer.Length, SocketFlags.None);

                    var stop = Environment.TickCount + 100000;
                    while ((readBytes != readable) && (stop < Environment.TickCount))
                    {
                        readBytes += Socket.Receive(receiveBuffer, readBytes, receiveBuffer.Length - readBytes, SocketFlags.None);
                    }

                    if (readBytes > 0)
                    {
                        var s = new string(Encoding.GetChars(receiveBuffer, 0, readBytes));

                        {
                            var response = new StringBuilder();
                            var index = 1;
                            SEQUENCE_START:
                            var snmpDataLength = GetLength(ref receiveBuffer, ref index);

                            int snmpVersionType = receiveBuffer[index++]; // SNMP version type: Integer
                            var snmpVersionLength = GetLength(ref receiveBuffer, ref index);
                            int snmpVersion = receiveBuffer[index++]; // SNMP version 1

                            int communityNameType = receiveBuffer[index++]; // Community name type: String
                            var communityLength = GetLength(ref receiveBuffer, ref index);
                            var communityBytes = new byte[communityLength];
                            var sb = new StringBuilder();
                            for (var i = 0; i < communityBytes.Length; i++)
                            {
                                communityBytes[i] = receiveBuffer[index++]; // Community name in byte array format
                                sb.Append((char)communityBytes[i]);
                            }
                            //string community = sb.ToString();
                            var method = (SnmpMethod)receiveBuffer[index++]; // SNMP response
                            var responseDataLength = GetLength(ref receiveBuffer, ref index);

                            int snmpResponseIdType = receiveBuffer[index++]; // SNMP Response ID type: Integer
                            int snmpResponseIdLength = receiveBuffer[index++]; // SNMP Response ID length
                            var snmpResponseId = new byte[snmpResponseIdLength];
                            for (var i = 0; i < snmpResponseId.Length; i++)
                            {
                                snmpResponseId[i] = receiveBuffer[index++]; // SNMP Response ID
                            }

                            int snmpErrorStatusType = receiveBuffer[index++]; // SNMP error status type: Integer
                            int snmpErrorStatusLength = receiveBuffer[index++]; // SNMP error status length
                            var snmpErrorStatus = new byte[snmpErrorStatusLength];
                            for (var i = 0; i < snmpErrorStatus.Length; i++)
                            {
                                snmpErrorStatus[i] = receiveBuffer[index++]; // SNMP error status

                                //if ((SnmpStatus)snmpErrorStatus[i] != SnmpStatus.Success)
                                response.AppendLine(((SnmpStatus)snmpErrorStatus[i]).ToString());
                            }

                            int snmpErrorIndexType = receiveBuffer[index++]; // SNMP error index type: Integer
                            int snmpErrorIndexLength = receiveBuffer[index++]; // SNMP error index length
                            var snmpErrorIndex = new byte[snmpErrorStatusLength];
                            for (var i = 0; i < snmpErrorIndex.Length; i++)
                                snmpErrorIndex[i] = receiveBuffer[index++]; // SNMP error index

                            index++; // Start of variable bindings sequence
                            var sizeOfVariableBinding = GetLength(ref receiveBuffer, ref index);
                            index++; // Start of first variable bindings sequence
                            var size = GetLength(ref receiveBuffer, ref index);

                            while (index < receiveBuffer.Length)
                            {
                                switch ((SnmpTypes)receiveBuffer[index])
                                {
                                    case SnmpTypes.IPAddress:
                                        index++;
                                        response.AppendLine("IP Address");
                                        response.Append("Length: ");
                                        var ipLength = GetLength(ref receiveBuffer, ref index);
                                        response.AppendLine(ipLength.ToString());
                                        response.Append("Value: ");
                                        for (var i = 0; i < ipLength; i++)
                                        {
                                            response.Append(receiveBuffer[index++]);
                                            if (i < ipLength - 1)
                                            {
                                                response.Append('.');
                                            }
                                        }
                                        response.AppendLine();
                                        break;
                                    case SnmpTypes.OctetString:
                                        index++;
                                        response.AppendLine("Octet string");
                                        var strLength = GetLength(ref receiveBuffer, ref index);
                                        response.AppendLine($"Length: {strLength}");
                                        response.Append("Value: ");
                                        for (var i = 0; i < strLength; i++)
                                        {
                                            response.Append((char)receiveBuffer[index++]);
                                        }
                                        response.AppendLine();
                                        break;
                                    case SnmpTypes.Null:
                                        //index++; // FIXME ?!?!?!
                                        index += 2; // Pelco Camera SNMP
                                        //response = null;
                                        break;
                                    case SnmpTypes.ObjectIdentifier:
                                        index++;
                                        response.AppendLine("Object identifier");
                                        response.Append("Length: ");
                                        var oidLength = GetLength(ref receiveBuffer, ref index);
                                        response.AppendLine(oidLength.ToString());
                                        response.Append("Value: ");
                                        var k = 0;
                                        while (k < oidLength)
                                        {
                                            if (k != 0)
                                            {
                                                response.Append('.');
                                                if (receiveBuffer[index] >= OidConverter.ByteHalf)
                                                {
                                                    var n = receiveBuffer[index] - OidConverter.ByteHalf;
                                                    // TODO: Fix this
                                                    //int oidNumber = receiveBuffer[index++] + n * OidConverter.ByteHalf; // SNMPv1? => SnmpPacket.cs // hibás képlet prefix növelés?
                                                    var oidNumber = receiveBuffer[++index] + n * OidConverter.ByteHalf; // SNMPv1? => SnmpPacket.cs // hibás képlet prefix növelés?
                                                    //int oidNumber = receiveBuffer[++index] + n * 256; // SNMPv2? => SnmpPacket.cs
                                                    response.Append(oidNumber.ToString());
                                                    k++;
                                                    index++;
                                                }
                                                else
                                                {
                                                    //response.Append((char)(receive_buffer[index++] + Convert.ToByte('0')));
                                                    response.Append(receiveBuffer[index++].ToString());
                                                }
                                            }
                                            else
                                            {
                                                response.Append("1.3");
                                                index++;
                                            }
                                            k++;
                                        }
                                        response.AppendLine();
                                        break;
                                    case SnmpTypes.SNMPSequenceStart:
                                        index++;
                                        goto SEQUENCE_START;
                                    //break;
                                    case SnmpTypes.Counter:
                                    case SnmpTypes.Gauge:
                                    case SnmpTypes.Integer_66:
                                    case SnmpTypes.Integer_67:
                                        var convertToTimespan = (SnmpTypes)receiveBuffer[index] == SnmpTypes.Integer_67;
                                        index++;
                                        response.AppendLine(convertToTimespan ? "TimeSpan" : "Integer");
                                        response.Append("Length: ");
                                        var intLength = GetLength(ref receiveBuffer, ref index);
                                        response.AppendLine(intLength.ToString());
                                        response.Append("Value: ");
                                        long bigValue = 0;
                                        for (var i = 0; i < intLength; i++)
                                            bigValue = bigValue * 256 + receiveBuffer[index++];
                                        if (convertToTimespan)
                                        {
                                            var ts = bigValue.ToTimeSpan();
                                            response.Append(ts.Days.ToString());
                                            response.Append(' ');
                                            response.Append($"{ts.Hours:00}");
                                            response.Append(':');
                                            response.Append($"{ts.Minutes:00}");
                                            response.Append(':');
                                            response.Append($"{ts.Seconds:00}");
                                            response.Append('.');
                                            response.Append($"{ts.Milliseconds:000}");
                                        }
                                        else response.Append(bigValue);
                                        response.AppendLine();
                                        break;
                                    default:
                                        response.AppendLine($"Unknown type ({receiveBuffer[index++]})");
                                        response.Append("Length: ");
                                        var length = GetLength(ref receiveBuffer, ref index);
                                        response.AppendLine(length.ToString());
                                        response.Append("Value: ");
                                        for (var i = 0; i < length; i++)
                                        {
                                            if (receiveBuffer[index] != 0) response.Append((char)receiveBuffer[index]);
                                            else response.Append("<NUL>");
                                            response.Append(" (");
                                            response.Append(receiveBuffer[index++].ToString());
                                            response.Append(") ");
                                        }
                                        response.AppendLine();
                                        break;
                                }
                                //index++;
                            }

                            var array = new byte[response.Length];
                            for (var i = 0; i < response.Length; i++)
                            {
                                array[i] = (byte)response[i];
                            }

                            OnDataArrived(new DataArrivedEventArgs(Tag, Socket, (IPEndPoint)Socket.RemoteEndPoint, array));
                        }
                    }
                }
                Thread.Sleep(1);
            }
        }

        private static int GetLength(ref byte[] array, ref int index)
        {
            var result = 0;
            if (array[index] > 0x80)
            {
                var n = (byte)(array[index] - 0x80);
                for (var i = 0; i < n; i++)
                {
                    result = result * 256 + array[++index];
                }
                index++;
            }
            else
            {
                result = array[index++];
            }
            return result;
        }
    }
}