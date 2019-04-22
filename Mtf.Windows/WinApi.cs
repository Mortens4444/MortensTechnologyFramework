using System;
using System.Runtime.InteropServices;

namespace Mtf.Windows
{
    public static class WinApi
    {
        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int[] lParam);
    }
}