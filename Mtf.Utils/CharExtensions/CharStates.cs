using System;
using System.Runtime.InteropServices;

namespace Mtf.Utils.CharExtensions
{
    public class CharStates
    {
        [DllImport("User32.dll")]
        public static extern short VkKeyScan(char ch);

        public int VirtualCodeAndShiftState { get; }

        public int ShiftState { get; }

        public int VirtualCode { get; }

        public CharStates(byte ch)
            : this(Convert.ToChar(ch))
        { }

        public CharStates(char ch)
        {
            VirtualCodeAndShiftState = VkKeyScan(ch);
            VirtualCode = VirtualCodeAndShiftState & 0x00FF;
            ShiftState = (VirtualCodeAndShiftState & 0xFF00) >> 8;
        }

        public static CharStates GetCharStates(byte ch)
        {
            return GetCharStates(Convert.ToChar(ch));
        }

        public static CharStates GetCharStates(char ch)
        {
            return new CharStates(ch);
        }
    }
}