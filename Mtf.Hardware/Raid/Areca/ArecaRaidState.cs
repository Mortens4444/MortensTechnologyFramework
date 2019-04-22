using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Mtf.Hardware.Raid.Areca.Enum;
using Mtf.Utils.EnumExtensions;

namespace Mtf.Hardware.Raid.Areca
{
    public class ArecaRaidState : CommandParser
    {
        public RaidSet[] RaidSets { get; }
        public static NumberFormatInfo NumberFormatInfo { get; private set; }
        public string MainProcessor { get; private set; }
        public string CpuiCacheSize { get; private set; }
        public string CpudCacheSize { get; private set; }
        public string SystemMemory { get; private set; }
        public string FirmwareVersion { get; private set; }
        public string BootRomVersion { get; private set; }
        public string SerialNumber { get; private set; }
        public string ControllerName { get; private set; }
        public string IpAddress { get; private set; }
        public string GuiErrorMessage { get; private set; }
        public ArecaEvent[] Events { get; }

        public ArecaRaidState()
        {
            information = new Dictionary<string, Tuple<string, Type>>
            {
                { "Main Processor", new Tuple<string, Type>(nameof(MainProcessor), typeof(string)) },
                { "CPU ICache Size", new Tuple<string, Type>(nameof(CpuiCacheSize), typeof(string)) },
                { "CPU DCache Size", new Tuple<string, Type>(nameof(CpudCacheSize), typeof(string)) },
                { "System Memory", new Tuple<string, Type>(nameof(SystemMemory), typeof(string)) },
                { "Firmware Version", new Tuple<string, Type>(nameof(FirmwareVersion), typeof(string)) },
                { "BOOT ROM Version", new Tuple<string, Type>(nameof(BootRomVersion), typeof(string)) },
                { "Serial Number", new Tuple<string, Type>(nameof(SerialNumber), typeof(string)) },
                { "Controller Name", new Tuple<string, Type>(nameof(ControllerName), typeof(string)) },
                { "Current IP Address", new Tuple<string, Type>(nameof(IpAddress), typeof(string)) },
                { "GuiErrMsg", new Tuple<string, Type>(nameof(GuiErrorMessage), typeof(string)) }
            };

            var directory = IntPtr.Size == 8 ? Environment.GetEnvironmentVariable("ProgramW6432") : Environment.GetEnvironmentVariable("ProgramFiles");
            var results = File.Utils.Search(directory, "cli.exe");
            if (results.Count == 0)
            {
                throw new FileNotFoundException("File not found: cli.exe");
            }
            CliPath = results[0];
            NumberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };

            var lines = GetLines(RaidSetInfo());
            var i = GetStartLine(lines);

            int db = 0, si = i;
            while (i < lines.Length && lines[i].IndexOf("==") == NotFound)
            {
                db++;
                i++;
            }
            i = si;
            RaidSets = new RaidSet[db];
            for (var j = 0; j < RaidSets.Length; j++)
            {
                var values = ElliminateCharsAndCreateArray(lines[i++], ' ');
                RaidSets[j] = new RaidSet(Convert.ToInt32(values[0]), values[values.Length - 2]);
            }

            lines = GetLines(VolumeSetInfo());
            i = GetStartLine(lines);

            db = 0;
            si = i;
            while (i < lines.Length && lines[i].IndexOf("==") == NotFound)
            {
                db++;
                i++;
            }
            i = si;
            for (var j = 0; j < db; j++)
            {
                var values = ElliminateCharsAndCreateArray(lines[i++], ' ');
                var raidSetId = Convert.ToInt32(values[values.Length - 5]);
                var rs = GetRaidSet(raidSetId);
                rs?.AddVolume(Convert.ToInt32(values[0]));
            }

            ProcessInfo(Info());
            Events = GetEvents();
        }

        public override string ToString()
        {
            var toString = new StringBuilder();
            toString.AppendLine(Info());
            if (RaidSets?.Length > 0)
            {
                toString.AppendLine("RAIDSets");
                foreach (var raidSet in RaidSets)
                {
                    toString.AppendLine(raidSet .ToString());
                }
            }
            return toString.ToString();
        }

        public RaidSet GetRaidSet(int raidSetId)
        {
            return RaidSets.FirstOrDefault(t => t.RaidSetId == raidSetId);
        }

        public string RaidSetInfo()
        {
            return ExcecuteCommand("rsf info");
        }

        public string VolumeSetInfo()
        {
            return ExcecuteCommand("vsf info");
        }

        public string DiskInfo()
        {
            return ExcecuteCommand("disk info");
        }

        public string HardwareMonitorInfo()
        {
            return ExcecuteCommand("hw info");
        }

        public void ChangeBackgroundTaskPriority(Priority priority)
        {
            ExcecuteCommand($"sys changept p={priority.GetSecondaryValue()}");
        }

        public void ChangeHostMode(HostMode hostMode)
        {
            ExcecuteCommand($"sys mode p={(int)hostMode}");
        }

        public string Info()
        {
            return ExcecuteCommand("sys info");
        }

        public string NetworkInfo()
        {
            return ExcecuteCommand("net info");
        }

        public ArecaEvent[] GetEvents()
        {
            var lines = GetLines(ExcecuteCommand("event info"));

            var i = GetStartLine(lines);

            int db = 0, si = i;
            while (i < lines.Length && lines[i].IndexOf("==") == NotFound)
            {
                db++;
                i++;
            }

            i = si;
            var events = new ArecaEvent[db];
            for (var j = 0; j < events.Length; j++)
            {
                var values = ElliminateCharsAndCreateArray(lines[i], ' ');
                var eventTime = Convert.ToDateTime(values[0] + ' ' + values[1]);
                var device = values[2];
                var message = String.Join(" ", values.Skip(3));
                events[j] = new ArecaEvent(eventTime, device, message);
                i++;
            }
            return events;
        }

        public void ClearEvents()
        {
            ExcecuteCommand("event clear");
        }

        public void UpdateFirmware(string firmwareFile)
        {
            ExcecuteCommand($"sys updatefw path={firmwareFile}");
        }

        public void Set_DHCP_State(EnableDisable state)
        {
            ExcecuteCommand($"net dhcp p={(int)EnableDisable.Enable}");
        }

        public void Set_DHCP_State(string ipAddress)
        {
            ExcecuteCommand($"net ipaddr p={ipAddress}");
        }

        public void ChangePassword(string password)
        {
            ExcecuteCommand($"sys changepwd p={password}");
        }

        public void SetBeeperState(BeeperState state)
        {
            ExcecuteCommand($"sys beeper {(int)state}");
        }
    }
}