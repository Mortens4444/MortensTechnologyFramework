using System.Runtime.InteropServices;

namespace Mtf.Network
{
    [StructLayout(LayoutKind.Sequential)]
    public struct IpHdr
    {
        [BitfieldLength(4)]
        public uint ihl;
        [BitfieldLength(4)]
        public uint version;
        public byte tos;
        public uint tot_len;
        public long id;
        public uint frag_off;
        public byte ttl;
        public byte protocol;
        public uint check;
        public uint saddr;
        public uint daddr;
    }
}