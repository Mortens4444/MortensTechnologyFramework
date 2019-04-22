using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Mtf.Windows.Enum;

namespace Mtf.Windows.Hook
{
    public class MouseHook : HookBase
    {
        public event MouseEventHandler OnMouseActivity;

        private readonly IntPtr mouseHandle;
        private readonly IntPtr moduleHandle;

        private readonly KeyboardHook.HookProc mouseHookProc;
        private MouseButtons buttonStates;

        public MouseHook()
        {
            moduleHandle = GetMainModuleHandle();
            mouseHookProc = MouseHookProcedure;
            mouseHandle = WinApi.SetWindowsHookEx(HookType.WH_MOUSE_LL, mouseHookProc, moduleHandle, 0);

            buttonStates = MouseButtons.None;
            if (mouseHandle == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        public void Stop()
        {
            if (mouseHandle == IntPtr.Zero) return;
            if (!WinApi.UnhookWindowsHookEx(mouseHandle))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        private int MouseHookProcedure(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && OnMouseActivity != null)
            {
                var mouseHookStruct = (MouseDllHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseDllHookStruct));

                //MouseButtons button = MouseButtons.None;
                short mouseDelta = 0;
                var msg = (WindowMessages)wParam.ToInt32();

                switch (msg)
                {
                    case WindowMessages.WM_LBUTTONDOWN:
                        buttonStates |= MouseButtons.Left;
                        break;
                    case WindowMessages.WM_RBUTTONDOWN:
                        buttonStates |= MouseButtons.Right;
                        break;
                    case WindowMessages.WM_MBUTTONDOWN:
                        buttonStates |= MouseButtons.Middle;
                        break;
                    case WindowMessages.WM_LBUTTONUP:
                        buttonStates -= MouseButtons.Left;
                        break;
                    case WindowMessages.WM_RBUTTONUP:
                        buttonStates -= MouseButtons.Right;
                        break;
                    case WindowMessages.WM_MBUTTONUP:
                        buttonStates -= MouseButtons.Middle;
                        break;
                    case WindowMessages.WM_MOUSEWHEEL:
                        mouseDelta = (short)((mouseHookStruct.mouseData >> 16) & 0xFFFF);
                        break;
                }

                var clickCount = 0;
                if (buttonStates != MouseButtons.None)
                {
                    if (msg == WindowMessages.WM_LBUTTONDBLCLK || msg == WindowMessages.WM_RBUTTONDBLCLK ||
                        msg == WindowMessages.WM_MBUTTONDBLCLK)
                    {
                        clickCount = 2;
                    }
                    else
                    {
                        clickCount = 1;
                    }
                }

                var e = new MouseEventArgs(buttonStates, clickCount, mouseHookStruct.pt.X, mouseHookStruct.pt.Y, mouseDelta);
                OnMouseActivity(this, e);
            }
            return WinApi.CallNextHookEx(mouseHandle, nCode, wParam, lParam);
        }
    }
}