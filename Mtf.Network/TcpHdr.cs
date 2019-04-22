//#define __BYTE_ORDER __LITTLE_ENDIAN // Intel Processors
//#define __BYTE_ORDER __BIG_ENDIAN // Motorola Processors
using System.Runtime.InteropServices;

namespace Mtf.Network
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TcpHdr
    {
        public short source;
        public short dest;
        public ulong seq;
        public ulong ack_seq;
#if __BYTE_ORDER == __LITTLE_ENDIAN
        [BitfieldLength(4)]
        public ushort res1;
        [BitfieldLength(4)]
        public ushort doff;
        [BitfieldLength(1)]
        public ushort fin;
        [BitfieldLength(1)]
        public ushort syn;
        [BitfieldLength(1)]
        public ushort rst;
        [BitfieldLength(1)]
        public ushort psh;
        [BitfieldLength(1)]
        public ushort ack;
        [BitfieldLength(1)]
        public ushort urg;
        [BitfieldLength(2)]
        public ushort res2;
#elif __BYTE_ORDER == __BIG_ENDIAN
        [BitfieldLength(4)]
        public ushort doff;
        [BitfieldLength(4)]
        public ushort res1;
        [BitfieldLength(2)]
        public ushort res2;
        [BitfieldLength(1)]
        public ushort urg;
        [BitfieldLength(1)]
        public ushort ack;
        [BitfieldLength(1)]
        public ushort psh;
        [BitfieldLength(1)]
        public ushort rst;
        [BitfieldLength(1)]
        public ushort syn;
        [BitfieldLength(1)]
        public ushort fin;
#endif
        public short window;
        public ushort check;
        public ushort urg_ptr;
    }
}