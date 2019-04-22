namespace Mtf.Hardware.Raid.Amcc3Ware
{
    public class Enclosure
    {
        public int CtlID;
        public string Name;
        public int Slots;
        public int Drives;
        public int Fans;
        public int TSUnits;
        public int PSUnits;
        public int Alarms;

        public Enclosure(int controllerId, string name, int slots, int drives, int fans, int tsUnits, int psUnits, int alarms)
        {
            CtlID = controllerId;
            Name = name;
            Slots = slots;
            Drives = drives;
            Fans = fans;
            TSUnits = tsUnits;
            PSUnits = psUnits;
            Alarms = alarms;
        }

        public override string ToString()
        {
            return $"Encolsure {CtlID}: {Name}";
        }
    }
}