using System;
using System.Runtime.InteropServices;

namespace Mtf.Windows.Hook
{
    [StructLayout(LayoutKind.Sequential)]
    public class KbDllHookStruct
    {
        public uint vkCode;
        public uint scanCode;
        public KbDllHookStructFlags flags;
        public uint time;
        public UIntPtr dwExtraInfo;
    }
}