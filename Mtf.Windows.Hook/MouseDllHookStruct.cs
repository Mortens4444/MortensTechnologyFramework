using System;
using System.Runtime.InteropServices;

namespace Mtf.Windows.Hook
{
    [StructLayout(LayoutKind.Sequential)]
    public class MouseDllHookStruct
    {
        public Point pt;
        public int mouseData; // Must be int!!!
        public int flags;
        public int time;
        public UIntPtr dwExtraInfo;
    }
}