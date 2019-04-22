using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Mtf.Utils.Enum;

namespace Mtf.Utils.CharExtensions
{
    public static class Simulator
    {
        [DllImport("User32.dll")]
        public static extern void keybd_event(VirtualKeyCodes bVk, byte bScan, KeyboardEventFlags dwFlags, UIntPtr dwExtraInfo);

        [DllImport("User32.dll")]
        public static extern void keybd_event(int bVk, uint bScan, KeyboardEventFlags dwFlags, UIntPtr dwExtraInfo);

        [DllImport("User32.dll")]
        public static extern IntPtr LoadKeyboardLayout(string pwszKlid, LoadKeyboardLayoutFlags Flags);

        [DllImport("User32.dll")]
        public static extern uint MapVirtualKey(VirtualKeyCodes uCode, MapVirtualKeyMapTypes uMapType);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern bool UnloadKeyboardLayout(IntPtr hkl);
        
        private const string NotAvailable = "N/A";

        public static void SimulateKeyPress(this char ch)
        {
            var cs = new CharStates(ch);
            if (cs.ShiftState == (int) KeyModifier.Shift)
            {
                keybd_event(VirtualKeyCodes.VK_SHIFT, 0, KeyboardEventFlags.KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
            }
            if (cs.ShiftState == (int) KeyModifier.Alt)
            {
                keybd_event(VirtualKeyCodes.VK_MENU, 0, KeyboardEventFlags.KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
            }
            keybd_event(cs.VirtualCode, 0, KeyboardEventFlags.NOTHING, UIntPtr.Zero);
            keybd_event(cs.VirtualCode, 0, KeyboardEventFlags.KEYEVENTF_KEYUP, UIntPtr.Zero);
            if (cs.ShiftState == (int) KeyModifier.Alt)
            {
                keybd_event(VirtualKeyCodes.VK_MENU, 0, KeyboardEventFlags.KEYEVENTF_EXTENDEDKEY_AND_KEYUP, UIntPtr.Zero);
            }
            if (cs.ShiftState == (int) KeyModifier.Shift)
            {
                keybd_event(VirtualKeyCodes.VK_SHIFT, 0, KeyboardEventFlags.KEYEVENTF_EXTENDEDKEY_AND_KEYUP, UIntPtr.Zero);
            }
        }

        public static ArrayList GetKeyboardLayoutCharachters(string layout = null)
        {
            var hkl = IntPtr.Zero;
            if (!String.IsNullOrEmpty(layout))
            {
                hkl = LoadKeyboardLayout(layout, LoadKeyboardLayoutFlags.KLF_ACTIVATE);
            }

            var chars = new ArrayList();
            var byteValue = 0;
            while (byteValue < 256)
            {
                var charDetails = new ArrayList();
                var ch = Convert.ToChar(byteValue);

                // The character
                charDetails.Add(Convert.ToString(ch));

                // The byte value of the character
                charDetails.Add(byteValue);

                // Hexadecimal value of the character
                var hexaForm = Convert.ToString(byteValue, 16).ToUpper();
                if (hexaForm.Length == 1)
                {
                    hexaForm = "0" + hexaForm;
                }
                charDetails.Add(hexaForm);

                // Virtual code
                var cs = new CharStates(ch);
                charDetails.Add(cs.VirtualCode);

                // KeyData
                var keyData = (Keys)cs.VirtualCode;
                charDetails.Add(Convert.ToString(keyData));

                // VirtualCode to char
                var nch = MapVirtualKey((VirtualKeyCodes)cs.VirtualCode, MapVirtualKeyMapTypes.MAPVK_VK_TO_CHAR);
                charDetails.Add(Convert.ToString(Convert.ToChar(nch)));

                // Scan code
                var nch2 = MapVirtualKey((VirtualKeyCodes)cs.VirtualCode, MapVirtualKeyMapTypes.MAPVK_VK_TO_VSC);
                charDetails.Add(Convert.ToString(nch2));
                charDetails.Add(Convert.ToString(nch));

                nch2 = MapVirtualKey((VirtualKeyCodes)nch2, MapVirtualKeyMapTypes.MAPVK_VSC_TO_VK_EX);
                charDetails.Add(Convert.ToString(nch2));

                AddKeyModifierToCharDetails(charDetails, cs.ShiftState, ch, KeyModifier.Shift);
                AddKeyModifierToCharDetails(charDetails, cs.ShiftState, ch, KeyModifier.Alt);
                AddKeyModifierToCharDetails(charDetails, cs.ShiftState, ch, KeyModifier.Ctrl);

                chars.Add(charDetails);
                byteValue++;
            }
            if (hkl != IntPtr.Zero)
            {
                UnloadKeyboardLayout(hkl);
            }
            return chars;
        }

        private static void AddKeyModifierToCharDetails(IList charDetails, int shiftState, char ch, KeyModifier modifier)
        {
            try
            {
                charDetails.Add(shiftState == (int)modifier ? ch.ModifiedChar(KeyModifier.NoModifier) : ch.ModifiedChar(modifier));
            }
            catch
            {
                charDetails.Add(NotAvailable);
            }
        }

        public static char ModifiedChar(this char value, KeyModifier modifier)
        {
            var cs = new CharStates(value);

            var i = 0;
            while (i < 256)
            {
                var testedChar = Convert.ToChar(i);
                var pcs = new CharStates(testedChar);
                if (pcs.VirtualCode == cs.VirtualCode && pcs.ShiftState == (int)modifier)
                {
                    return testedChar;
                }
                i++;
            }
            return Convert.ToChar(0);
        }
    }
}