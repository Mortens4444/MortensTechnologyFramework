using System;

namespace Mtf.Hardware.Raid.Amcc3Ware
{
    public class SubUnit : CommandExecutor
    {
        protected const int NotFound = -1;
		public const string Unitname = "UnitName ";
        public const string Unittype = "UnitType";
        public const string Unitvport = "UnitVPort";
        public const string STATUS = "Status ";

        public int CtlID;
        public int UnitID;
        //public int[] SubUnitID;
        public string UnitName;
        public string VIM;
        public string UnitType;
        public string Status;
        public string Cmpl;
        public string Stripe;
        public string VPort;
        public double SizeInGB;
        public ulong? Blocks;
        public bool OK;

        public SubUnit(int controllerId, string unitName, string unitType, string status, string cmpl, string vport, string stripe, double sizeInGb)
        {
            CtlID = controllerId;
            UnitName = unitName;

            var index1 = unitName.IndexOf('-');
            var index2 = unitName.IndexOf('/');
            if (index1 == NotFound && index2 == NotFound)
            {
                UnitID = Convert.ToInt32(unitName.Substring(1));
            }
            else
            {
                var index = index1 != NotFound ? index1: index2;
                UnitID = Convert.ToInt32(unitName.Substring(1, index - 1));
            }

            /*int db = 0;
            for (int i = 2; i < this.UnitName.Length; i += 2)
            {
                if (this.UnitName[i] == ch) db++;
            }
            this.SubUnitID = new int[db];
            db = 0;
            for (int i = 3; i < this.UnitName.Length; i += 2)
            {
                this.SubUnitID[db++] = Convert.ToInt32(this.UnitName[i] - '0');
            }*/
            VPort = vport;
            UnitType = unitType;
            Status = status;
            Cmpl = cmpl;
            Stripe = stripe;
            SizeInGB = sizeInGb;
            OK = status == "OK";
        }

        public SubUnit(int controllerId, string unitName, string unitType, string status, string cmpl, string vim, string vport, string stripe, double sizeInGb)
            : this (controllerId, unitName, unitType, status, cmpl, vport, stripe, sizeInGb)
        {
            VIM = vim;
        }

        public SubUnit(int controllerId, string unitName, string unitType, string status, string cmpl, string port, string stripe, double sizeInGb, ulong? blocks)
            : this(controllerId, unitName, unitType, status, cmpl, port, stripe, sizeInGb)
        {
            Blocks = blocks;
        }

        public override string ToString()
        {
            return $"{Unitname} {UnitName}, {Unittype} {UnitType}, {Unitvport} {VPort}, {STATUS} {Status}";
        }
    }

}