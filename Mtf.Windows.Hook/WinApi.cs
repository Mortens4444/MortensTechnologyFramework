using System;
using System.Runtime.InteropServices;
using Mtf.Utils.Enum;

namespace Mtf.Windows.Hook
{
    public static class WinApi
    {
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("User32.dll")]
        public static extern int CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("User32.dll")]
        public static extern short GetKeyState(VirtualKeyCodes nVirtKey);

        [DllImport("User32.dll")]
        public static extern IntPtr SetWindowsHookEx(HookType idHook, KeyboardHook.HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("User32")]
        public static extern AsciiReturnCode ToAscii(uint uVirtKey, uint uScanCode, byte[] lpKeyState, byte[] lpChar, uint uFlags);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

    }
}