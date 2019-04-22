using System;
using Mtf.Hardware.Raid.Amcc3Ware.Enum;
using Mtf.Utils.DateExtensions;

namespace Mtf.Hardware.Raid.Amcc3Ware
{
    public class SelfTest
    {
        public int Slot;
        public Day Day;
        public TimeSpan Hour;
        public string UDMA;
        public string SMART;

        public SelfTest(int slot, Day day, TimeSpan hour, string udma, string smart)
        {
            Day = day;
            Hour = hour;
            Slot = slot;
            SMART = smart;
            UDMA = udma;
        }

        public override string ToString()
        {
            return $"On {Day} at {Hour.TotalHours}";
        }
    }
}