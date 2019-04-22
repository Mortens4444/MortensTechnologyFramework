using System;
using System.Collections.Generic;

namespace Mtf.Network.Packets.Snmp
{
    public class OidConverter
    {
        public const int ByteHalf = 128;

        public byte[] ToByteArray(string oidString)
        {
            //return new byte[] { 1, 3, 6, 1, 4, 1, 1, 20, 40, 1, 1, 1 };
            //return new byte[] { 1, 3, 6, 1, 2, 1, 1, 1, 0 };
            //return new byte[] { 1, 3, 6, 1, 4, 1, 129, 195, 106, 1, 1, 1 };
            //return new byte[] { 1, 3, 6, 1, 4, 1, 129, 67, 106, 1, 1, 1 };
            //return new byte[] { 1, 3, 6, 1, 4, 1, 195, 106, 1, 1, 1 };
            //return new byte[] { 1, 3, 6, 1, 4, 1, 225, 129, 106, 1, 1, 1 };

            var oid = new List<byte>();
            var integers = oidString.Split(new [] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var integer in integers)
            {
                int temp = Convert.ToInt16(integer);
                if (temp >= ByteHalf)
                {
                    oid.Add(Convert.ToByte(ByteHalf + temp / ByteHalf));
                    oid.Add(Convert.ToByte(temp - temp / ByteHalf * ByteHalf));
                }
                else
                {
                    oid.Add(Convert.ToByte(temp));
                }
            }
            return oid.ToArray();
        }
    }
}