using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;
using Mtf.Utils.Enum;
using Mtf.Windows.Enum;

namespace Mtf.Windows.Hook
{
    public class KeyboardHook : HookBase
    {
        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        private readonly IntPtr keyboardHandle;
        private readonly IntPtr moduleHandle;
        private bool leftAlt;
        private bool leftControl;
        private bool leftShift;
        private bool rightAlt;
        private bool rightControl;
        private bool rightShift;
        private bool alt;
        private bool control;
        private bool shift;

        public event KeyEventHandler KeyDown;
        public event KeyPressEventHandler KeyPress;
        public event KeyEventHandler KeyUp;

        private readonly HookProc keyboardHookProc;

        public KeyboardHook()
        {
            moduleHandle = GetMainModuleHandle();
            keyboardHookProc = KeyboardHookProcedure;
            keyboardHandle = WinApi.SetWindowsHookEx(HookType.WH_KEYBOARD_LL, keyboardHookProc, moduleHandle, 0);
            if (keyboardHandle == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        public void Stop()
        {
            if (keyboardHandle == IntPtr.Zero) return;
            if (!WinApi.UnhookWindowsHookEx(keyboardHandle))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        private int KeyboardHookProcedure(int nCode, IntPtr wParam, IntPtr lParam)
        {
            var handled = false;
            //bool isShiftDown = ((WinApi.GetKeyState(VirtualKeyCodes.VK_SHIFT) & 0x80) == 0x80 ? true : false);
            //bool isAltDown = ((WinApi.GetKeyState(VirtualKeyCodes.VK_MENU) & 0x80) == 0x80 ? true : false);
            //bool isControlDown = ((WinApi.GetKeyState(VirtualKeyCodes.VK_CONTROL) & 0x80) == 0x80 ? true : false);

            var msg = (WindowMessages)wParam.ToInt32();

            if (nCode >= 0 && (KeyDown != null || KeyUp != null || KeyPress != null))
            {
                var myKeyboardHookStruct = (KbDllHookStruct)Marshal.PtrToStructure(lParam, typeof(KbDllHookStruct));
                if (KeyDown != null && (msg == WindowMessages.WM_KEYDOWN || msg == WindowMessages.WM_SYSKEYDOWN))
                {
                    handled = HandleKeyDown(myKeyboardHookStruct);
                }

                if (KeyPress != null && msg == WindowMessages.WM_KEYDOWN)
                {
                    //var isShiftDown = (WinApi.GetKeyState(VirtualKeyCodes.VK_SHIFT) & 0x80) == 0x80;
                    var isCapsLockDown = WinApi.GetKeyState(VirtualKeyCodes.VK_CAPITAL) != 0;

                    var keyStates = new byte[256];
                    WinApi.GetKeyboardState(keyStates);
                    var character = new byte[2];

                    if (WinApi.ToAscii(myKeyboardHookStruct.vkCode, myKeyboardHookStruct.scanCode, keyStates, character, (uint)myKeyboardHookStruct.flags) == AsciiReturnCode.OneCharacter)
                    {
                        if (character.Length > 0)
                        {
                            var key = (char)character[0];
                            if (isCapsLockDown ^ shift && Char.IsLetter(key))
                            {
                                key = Char.ToUpper(key);
                            }
                            var e = new KeyPressEventArgs(key);
                            KeyPress(this, e);
                            handled |= e.Handled;
                        }
                    }
                }

                if (KeyUp != null && (msg == WindowMessages.WM_KEYUP || msg == WindowMessages.WM_SYSKEYUP))
                {
                    handled |= HandleKeyUp(myKeyboardHookStruct);
                }
            }

            return handled ? 1 : WinApi.CallNextHookEx(keyboardHandle, nCode, wParam, lParam);
        }

        private bool HandleKeyDown(KbDllHookStruct myKeyboardHookStruct)
        {
            var keyData = (Keys) myKeyboardHookStruct.vkCode;

            switch (keyData.ToString())
            {
                case "LMenu":
                    leftAlt = true;
                    break;
                case "LControlKey":
                    leftControl = true;
                    break;
                case "LShiftKey":
                    leftShift = true;
                    break;
                case "RMenu":
                    rightAlt = true;
                    break;
                case "RControlKey":
                    rightControl = true;
                    break;
                case "RShiftKey":
                    rightShift = true;
                    break;
            }
            alt = leftAlt || rightAlt;
            control = leftControl || rightControl;
            shift = leftShift || rightShift;

            keyData = ModifiyKeyData(keyData);
            var e = new KeyEventArgs(keyData);
            KeyDown(this, e);
            return e.Handled;
        }

        private bool HandleKeyUp(KbDllHookStruct myKeyboardHookStruct)
        {
            var keyData = (Keys) myKeyboardHookStruct.vkCode;

            switch (keyData.ToString())
            {
                case "LMenu":
                    leftAlt = false;
                    alt = rightAlt;
                    break;
                case "LControlKey":
                    leftControl = false;
                    control = rightControl;
                    break;
                case "LShiftKey":
                    leftShift = false;
                    shift = rightShift;
                    break;
                case "RMenu":
                    rightAlt = false;
                    alt = leftAlt;
                    break;
                case "RControlKey":
                    rightControl = false;
                    control = leftControl;
                    break;
                case "RShiftKey":
                    rightShift = false;
                    shift = leftShift;
                    break;
            }

            keyData = ModifiyKeyData(keyData);
            var e = new KeyEventArgs(keyData);
            KeyUp.Invoke(this, e);
            return e.Handled;
        }

        private Keys ModifiyKeyData(Keys keyData)
        {
            if (alt)
            {
                keyData |= Keys.Alt;
            }
            if (control)
            {
                keyData |= Keys.Control;
            }
            if (shift)
            {
                keyData |= Keys.Shift;
            }
            return keyData;
        }
    }
}
