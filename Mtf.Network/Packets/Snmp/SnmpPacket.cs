using System;
using System.Collections.Generic;
using System.Text;

namespace Mtf.Network.Packets.Snmp
{
    public class SnmpPacket
    {
        public const byte SnmpSequenceStart = 0x30;

        private readonly string community;
        private readonly SnmpMethod method;
        private readonly byte snmpVersion;
        private readonly uint packetId;
        private readonly byte errorStatus;
        private readonly byte errorIndex;

        public byte[] Payload { get; private set; }

        // SNMP version 1 is default
        public SnmpPacket(string community, string oidString, SnmpMethod method, byte snmpVersion = 0, uint packetId = 1, byte errorStatus = 0, byte errorIndex = 0)
        {
            this.community = community;
            this.method = method;
            this.snmpVersion = snmpVersion;
            this.packetId = packetId;
            this.errorStatus = errorStatus;
            this.errorIndex = errorIndex;
            var oidConverter = new OidConverter();
            var oid = oidConverter.ToByteArray(oidString);
            Create(oid);
        }

        private void Create(IReadOnlyList<byte> oid)
        {
            if (community == null)
            {
                throw new ArgumentNullException(nameof(community));
            }

            var packet = new byte[28 + community.Length + oid.Count];
            var index = 0;
            packet[index++] = SnmpSequenceStart;                    // SNMP sequence start
            packet[index++] = Convert.ToByte(packet.Length - 2);    // Length of the SNMP sequence

            packet[index++] = (byte)SnmpTypes.Gauge;	// type: Integer
            packet[index++] = 0x01;						// length
            packet[index++] = snmpVersion;		        // value

            packet[index++] = 0x04;                             // Community name type: String
            packet[index++] = Convert.ToByte(community.Length); // Community name length

            var communityBytes = Encoding.ASCII.GetBytes(community);
            foreach (var communityByte in communityBytes)
            {
                packet[index++] = communityByte;                 // Community name in byte array format
            }

            packet[index++] = (byte)method;
            packet[index++] = Convert.ToByte(19 + oid.Count); // Size of total OID

            // SNMP Request ID
            packet[index++] = (byte)SnmpTypes.Gauge;	// type
            packet[index++] = 0x04;						// length
            packet[index++] = (byte)(packetId >> 24);
            packet[index++] = (byte)(packetId >> 16);
            packet[index++] = (byte)(packetId >> 8);
            packet[index++] = (byte)(packetId & 0xFF);

            packet[index++] = (byte)SnmpTypes.Gauge;	// type
            packet[index++] = 0x01;						// length
            packet[index++] = errorStatus;		        // value

            // SNMP error index
            packet[index++] = (byte)SnmpTypes.Gauge;	// type
            packet[index++] = 0x01;						// length
            packet[index++] = errorIndex;			    // value

            packet[index++] = SnmpSequenceStart;                // Start of variable bindings sequence
            packet[index++] = Convert.ToByte(5 + oid.Count);   // Size of variable binding

            packet[index++] = SnmpSequenceStart;                // Start of first variable bindings sequence
            packet[index++] = Convert.ToByte(3 + oid.Count);   // Size
            packet[index++] = (byte)SnmpTypes.ObjectIdentifier; // Type: Object
            packet[index++] = Convert.ToByte(oid.Count - 1);   // Length

            // OID
            packet[index++] = Convert.ToByte(40 * oid[0] + oid[1]); // packet[index++] = 2B;
            for (var i = 2; i < oid.Count; i++)
            {
                packet[index++] = oid[i];
            }

            packet[index++] = (byte)SnmpTypes.Null;
            packet[index] = 0x00;

            Payload = packet;
        }
    }
}