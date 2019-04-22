using System;
using Mtf.Hardware.Raid.Amcc3Ware.Enum;

namespace Mtf.Hardware.Raid.Amcc3Ware
{
    public class Attributes
    {
        public string Driver; // Device driver associated with controller
        public string Model; // Controller model of controller
        public string Firmware; // Firmware version of controller
        public string Memory; // Size of memory installed on the controller
        public string BIOS; // BIOS version of controller
        public string Monitor; // Monitor (firmware boot-loader) version of controller
        public string Serial; // Serial number of controller
        public string PCB; // PCB (Printed Circuit Board) version of controller
        public string PChip; // PCI Interface Chip version of controller
        public string AChip; // ATA Interface Chip version of controller
        public int NumPorts; // Number of physical ports of controller
        public int NumUnits; // Number of units currently managed by controller
        public int NumDrives; // Number of drives currently managed by controller
        public JBOD_Export_Policy ExportJBOD; // JBOD Export policy (9000 series)
        public Cache_Policy OnDegrade; // Cache policy of degraded units (9000 series)
        public int SpinUp; // Number of concurrent disk that will spin up when the system is powered on (9000 series)
        public OnOff AutoCarve; // Over 2 TB Auto-Carve Policy (9000 series)
        public TimeSpan Stagger; // Time delay between each group of spinups at the power on (9000 series)

        /*public string UnitStatus;
        public string DriveStatus;
        public string AllUnitStatus;*/

        public Attributes(string driver, string model, string firmware, string memory, string bios, string monitor, string serial, string pcb, string pchip, string achip, int numports, int numunits, int numdrives, JBOD_Export_Policy exportjbod, Cache_Policy ondegrade, int spinup, OnOff autocarve, TimeSpan stagger)
        {
            Driver = driver;
            Model = model;
            Firmware = firmware;
            Memory = memory;
            BIOS = bios;
            Monitor = monitor;
            Serial = serial;
            PCB = pcb;
            PChip = pchip;
            AChip = achip;
            NumPorts = numports;
            NumUnits = numunits;
            NumDrives = numdrives;
            ExportJBOD = exportjbod;
            OnDegrade = ondegrade;
            SpinUp = spinup;
            AutoCarve = autocarve;
            Stagger = stagger;
        }

        public override string ToString()
        {
            return $"Model {Model}, Firmware {Firmware}";
        }
    }
}