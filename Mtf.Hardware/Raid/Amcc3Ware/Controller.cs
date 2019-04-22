using System;
using System.Text;
using Mtf.Hardware.Raid.Amcc3Ware.Enum;
using Mtf.Hardware.Raid.Areca.Enum;
using Mtf.Utils.DateExtensions;
using Mtf.Utils.StringExtensions;
using Decide = Mtf.Hardware.Raid.Amcc3Ware.Enum.Decide;

namespace Mtf.Hardware.Raid.Amcc3Ware
{
    public class Controller: CommandExecutor
    {
        private const int NotFound = -1;
        private const string On = "ON";
        private const string Off = "OFF";
        private const string Yes = "YES";
        private const string No = "NO";

        public const string NAME = "Name ";
        public const string MODEL = "Model";
        public const string NotOptimalUnits = "Not optimal units";

        public int CtlID;
        public string Name;
        public string Model;
        public int NumberOfPorts;
        public int NumberOfDrives;
        public int NumberOfUnits;
        public int NumberOfNotOptimalUnits;
        public int R_Rate;
        public int? V_Rate;
        public string BBU_string;

        public string RescanOutput;
        public Unit[] Units;
        public Port[] Ports;
        public BBU BBU;
        public Attributes Attibutes;
        public Alarm[] Alarms;
        public Schedule[] CurrentRebuildBackgroundTaskSchedules;
        public Schedule[] CurrentVerifyBackgroundTaskSchedules;
        public SelfTest[] SelfTests;
        public Enclosure[] Enclosures;

        public Controller(string name, string model, int numberOfPorts, int numberOfDrives, int numberOfUnits, int numberOfNotOptimalUnits, int rRate, int? vRate, string bbu)
        {
            Enclosures = new Enclosure[0];
            Name = name;
            CtlID = Convert.ToInt32(Name.Substring(1)); // Convert.ToInt32(this.Name[1] - '0');

            Model = model;
            NumberOfPorts = numberOfPorts;
            NumberOfDrives = numberOfDrives;
            NumberOfUnits = numberOfUnits;
            NumberOfNotOptimalUnits = numberOfNotOptimalUnits;
            R_Rate = rRate;
            V_Rate = vRate;
            BBU_string = bbu;
        }

        public override string ToString()
        {
            return $"{NAME}: {Name}, {MODEL}: {Model}, {NotOptimalUnits} {NumberOfNotOptimalUnits}";
        }

        public string InformationAboutController()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.InfoContoller, CtlID);
        }

        public string AddRebuild(Day day, byte hour, TimeSpan duration)
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.AddNewRebuildTask, CtlID, new DayConverter().GetStringFromDay(day), hour.ToString(), Math.Truncate(duration.TotalHours).ToString());
        }

        public string DeleteRebuild(int slotId)
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.DeleteRebuild, CtlID, slotId.ToString());
        }

        public string AddSelftest(Day day, byte hour)
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.AddNewSelfTestTask, CtlID, new DayConverter().GetStringFromDay(day), hour.ToString());
        }

        public string DeleteSelfTest(int slotId)
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.DeleteSelfTest, CtlID, slotId.ToString());
        }

        public string AddVerify(Day day, byte hour, TimeSpan duration)
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.AddNewVerifyTask, CtlID, new DayConverter().GetStringFromDay(day), hour.ToString(), Math.Truncate(duration.TotalHours).ToString());
        }

        public string DeleteVerify(int slotId)
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.DeleteVerify, CtlID, slotId.ToString());
        }

        public void GetSelfTests()
        {
            var output = ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.CurrentSelfTestBackgroundTaskSchedule, CtlID);
            SelfTests = AMCC_3Ware_State.GetSelfTests(output);
        }

        public string EnableRebuildSchedules()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.EnableRebuildSchedulesOnController, CtlID);
        }

        public string DisableRebuildSchedules()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.DisableRebuildSchedulesOnController, CtlID);
        }

        public string SetRebuildSchedulesPriority(Priority priority)
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.SetRebuildPrioritySchedulesOnController, CtlID, ((byte)priority).ToString());
        }

        public string EnableVerifySchedules()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.EnableVerifySchedulesOnController, CtlID);
        }

        public string DisableVerifySchedules()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.DisableVerifySchedulesOnController, CtlID);
        }

        public string EnableSelfTestSchedules()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.EnableSelfTestSchedulesOnController, CtlID);
        }

        public string DisableSelfTestSchedules()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.DisableSelfTestSchedulesOnController, CtlID);
        }

        public string EnableUDMASelfTestSchedules()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.EnableUDMASelfTestSchedulesOnController, CtlID);
        }

        public string DisableUDMASelfTestSchedules()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.DisableUDMASelfTestSchedulesOnController, CtlID);
        }

        public string EnableSMARTSelfTestSchedules()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.EnableSMARTSelfTestSchedulesOnController, CtlID);
        }

        public string SetJBODExportPolicyOn()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.JBODExportPolicyOn, CtlID);
        }

        public string SetJBODExportPolicyOff()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.JBODExportPolicyOff, CtlID);
        }

        public string SetAutoCarvePolicy(OnOff state)
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.SetAutoCarvePolicy, CtlID, state.ToString().ToLower());
        }

        public string MediaScan(StartStop operation)
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.MediaScan, CtlID, operation.ToString().ToLower());
        }

        public string SetControllerBasedCachePolicy(Cache_Policy policy)
        {
            var p1 = policy == Cache_Policy.Off ? "cacheoff" : "follow";
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.SetControllerBasedCachePolicy, CtlID, p1);
        }

        public string SetControllerBasedDiskSpinUpPolicy(int numberOfDrives)
        {
            if (numberOfDrives < 1 || numberOfDrives > NumberOfDrives)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfDrives), $"Parameter must be between 1 and {NumberOfDrives}");
            }

            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.SetControllerBasedDiskSpinUpPolicy, CtlID, numberOfDrives.ToString());
        }

        public string SetControllerBasedDiskSpinUpStaggerTimePolicy(int seconds)
        {
            if (seconds < 0 || seconds > 60)
            {
                throw new ArgumentOutOfRangeException(nameof(seconds), "Parameter must be between 0 and 60");
            }

            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.SetControllerBasedDiskSpinUpStaggerTimePolicy, CtlID, seconds.ToString());
        }

        public string DisableSMARTSelfTestSchedules()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.DisableSMARTSelfTestSchedulesOnController, CtlID);
        }

        public string SetVerifySchedulesPriority(Priority priority)
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.SetVerifyPrioritySchedulesOnController, CtlID, ((byte)priority).ToString());
        }

        public void GetCurrentVerifyBackgroundTaskSchedules()
        {
            var output = ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.CurrentVerifyBackgroundTaskSchedule, CtlID);
            CurrentVerifyBackgroundTaskSchedules = AMCC_3Ware_State.GetSchedules(output);
        }

        public void GetCurrentRebuildBackgroundTaskSchedules()
        {
            var output = ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.CurrentRebuildBackgroundTaskSchedule, CtlID);
            CurrentRebuildBackgroundTaskSchedules = AMCC_3Ware_State.GetSchedules(output);
        }

        public string Diagnostics()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.ControllerDiagnostics, CtlID);
        }

        public void GetAlarmMessages()
        {
            var output = ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.ShowAlarmMessagesOfSpecifiedController, CtlID);
            GetAlarms(output);
        }

        public void GetUnitsPortsAndBBU()
        {
            var output = ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.SummaryInformationAboutTheSpecifiedController, CtlID);
            GetUnitsPortsAndBBU(output);
        }

        public void GetAttributes()
        {
            var output = ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.ShowAttributes, CtlID);
            GetAttributes(output);
        }

        public void RescanAllPortsAndReconstituteAllUnits()
        {
            RescanOutput = ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.RescanAllPortsAndReconstituteAllUnits, CtlID);
        }

        public void RescanAllPortsAndReconstituteAllUnitsDoNotInformOS()
        {
            RescanOutput = ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.RescanAllPortsAndReconstituteAllUnitsDoNotInformOS, CtlID);
        }

        public void Flush()
        {
            ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.WriteAllCachedDataToDisk, CtlID);
        }

        public void CleanUpForShutdown()
        {
            ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.CleanUpForShutdown, CtlID);
        }

        private void GetAttributes(string output_of___cx_show_attribute)
        {
            var lines = output_of___cx_show_attribute.SplitOnNewLines();

            if (lines.Length != 18)
            {
                return;
            }

            var ondegrade = Cache_Policy.Follow_Unit_Policy;
            var exportjbod = JBOD_Export_Policy.Not_supported;
            var autocarve = OnOff.Off;
            string driver = null, model = null, firmware = null, memory = null, bios = null, monitor = null, serial = null, pcb = null, pchip = null, achip = null;
            int numports = 0, numunits = 0, numdrives = 0, spinup = 0;
            var i = 0;
            while (i < lines.Length)
            {
                var j = lines[i].IndexOf(" = ") + 3;
                switch (i)
                {
                    case 0:
                        driver = lines[i].Substring(j);
                        break;
                    case 1:
                        model = lines[i].Substring(j);
                        break;
                    case 2:
                        firmware = lines[i].Substring(j);
                        break;
                    case 3:
                        memory = lines[i].Substring(j);
                        break;
                    case 4:
                        bios = lines[i].Substring(j);
                        break;
                    case 5:
                        monitor = lines[i].Substring(j);
                        break;
                    case 6:
                        serial = lines[i].Substring(j);
                        break;
                    case 7:
                        pcb = lines[i].Substring(j);
                        break;
                    case 8:
                        pchip = lines[i].Substring(j);
                        break;
                    case 9:
                        achip = lines[i].Substring(j);
                        break;
                    case 10:
                        numports = Convert.ToInt32(lines[i].Substring(j));
                        break;
                    case 11:
                        numunits = Convert.ToInt32(lines[i].Substring(j));
                        break;
                    case 12:
                        numdrives = Convert.ToInt32(lines[i].Substring(j));
                        break;
                    case 13:
                        switch (lines[i].Substring(j).ToUpper())
                        {
                            case On:
                                exportjbod = JBOD_Export_Policy.On;
                                break;
                            case Off:
                                exportjbod = JBOD_Export_Policy.Off;
                                break;
                        }
                        break;
                    case 14:
                        if (lines[i].Substring(j).ToUpper() != "FOLLOW UNIT POLICY")
                            ondegrade = Cache_Policy.Off;
                        break;
                    case 15:
                        spinup = Convert.ToInt32(lines[i].Substring(j));
                        break;
                    case 16:
                        switch (lines[i].Substring(j).ToUpper())
                        {
                            case On:
                                autocarve = OnOff.On;
                                break;
                            case Off:
                                autocarve = OnOff.Off;
                                break;
                        }
                        break;
                    case 17:
                        var stagger = new TimeSpan(0, 0, Convert.ToInt32(lines[i].Substring(j)));
                        Attibutes = new Attributes(driver, model, firmware, memory, bios, monitor, serial, pcb, pchip, achip, numports, numunits, numdrives, exportjbod, ondegrade, spinup, autocarve, stagger);
                        break;
                }
                i++;
            }
        }

        private void GetAlarms(string output_of___cx_show_alarms)
        {
            var lines = output_of___cx_show_alarms.SplitOnNewLines();

            var i = 0;
            while (i < lines.Length && lines[i].IndexOf("--") == NotFound)
            {
                i++;
            }
            i++;

            int db = 0, si = i;
            while (i < lines.Length)
            {
                if (lines[i] != String.Empty)
                {
                    db++;
                }
                i++;
            }
            i = si;

            if (i >= lines.Length)
            {
                return;
            }

            Alarms = new Alarm[db];
            var j = 0;
            while (j < db)
            {
                if (lines[i] != String.Empty)
                {
                    var values = ElliminateCharsAndCreateArray(lines[i], ' ');
                    DateTime? alarmTime = null;
                    string severity = null, lineSubstring = null;

                    if (values.Length > 4)
                    {
                        var dateStartIndex = lines[i].IndexOf('[') + 1;
                        var dateEndIndex = lines[i].IndexOf(']');
                        lineSubstring = lines[i].Substring(dateEndIndex + 2).Trim();
                        alarmTime = Convert.ToDateTime(lines[i].Substring(dateStartIndex, dateEndIndex - dateStartIndex));
                        severity = lineSubstring.Substring(0, lineSubstring.IndexOf(' '));
                    }
                    else
                    {
                        var index = lines[i].IndexOf('-') + 1;
                        index = lines[i].IndexOf('-', index + 1) + 1;
                        lineSubstring = lineSubstring.Substring(index).Trim();
                    }

                    var messageStartIndex = lineSubstring.IndexOf('(');
                    var message = messageStartIndex > NotFound ? lineSubstring.Substring(messageStartIndex) : lineSubstring.Substring(lineSubstring.IndexOf(' ')).Trim();
                    Alarms[j++] = new Alarm(alarmTime, severity, message);
                }
                i++;
            }
        }

        public void AddEnclosure(int controllerId, string name, int slots, int drives, int fans, int tsUnits, int psUnits, int alarms)
        {
            Array.Resize(ref Enclosures, Enclosures.Length + 1);
            Enclosures[Enclosures.Length - 1] = new Enclosure(controllerId, name, slots, drives, fans, tsUnits, psUnits, alarms);
        }

        //private void GetUnitsPortsAndBBU(string output_of___cx_show)
        public void GetUnitsPortsAndBBU(string output_of___cx_show)
        {
            Units = new Unit[NumberOfUnits];
            Ports = new Port[NumberOfPorts];

            var lines = output_of___cx_show.SplitOnNewLines();

            string[] values;
            var i = 0;
            while (i < lines.Length && lines[i].IndexOf("--") == NotFound) i++;
            i++;

            string status;
            var j = 0;
            //while ((i < lines.Length) && (lines[i].IndexOf("--") != NotFound))
            var IgnECC = lines[i - 2].IndexOf("IgnECC") > NotFound;
            var VIM = (lines[i - 2].IndexOf("%V/I/M") > NotFound);
            while (i < lines.Length && lines[i] != String.Empty)
            {
                values = ElliminateCharsAndCreateArray(lines[i++], ' ');
                if (values.Length != 9)
                {
                    continue;
                }

                var unit = values[0];
                var unitType = values[1];
                status = values[2];
                var cmpl = values[3];

                if (IgnECC)
                {
                    var stripe = values[4];
                    var size_in_gb = Convert.ToDouble(values[5], AMCC_3Ware_State.number_format_info);
                    var cache = values[6];
                    var averify = OnOffUnknown.Unknown;
                    switch (values[7])
                    {
                        case On:
                            averify = OnOffUnknown.On;
                            break;
                        case Off:
                            averify = OnOffUnknown.Off;
                            break;
                    }
                    var ign_ecc = OnOffUnknown.Unknown;
                    switch (values[8])
                    {
                        case On:
                            ign_ecc = OnOffUnknown.On;
                            break;
                        case Off:
                            ign_ecc = OnOffUnknown.Off;
                            break;
                    }
                    Units[j++] = new Unit(CtlID, unit, unitType, status, cmpl, stripe, size_in_gb, cache, averify, ign_ecc);
                }
                else if (VIM)
                {
                    var vim = values[4];
                    var stripe = values[5];
                    var size_in_gb = Convert.ToDouble(values[6], AMCC_3Ware_State.number_format_info);
                    var cache = values[7];
                    var averify = OnOffUnknown.Unknown;
                    switch (values[8])
                    {
                        case On:
                            averify = OnOffUnknown.On;
                            break;
                        case Off:
                            averify = OnOffUnknown.Off;
                            break;
                    }
                    Units[j++] = new Unit(CtlID, unit, unitType, status, cmpl, vim, stripe, size_in_gb, cache, averify);
                }
                else
                {
                    Units[j++] = new Unit(CtlID, unit, unitType, status, cmpl);
                }
            }
            i++;
            j = 0;
            //while ((i < lines.Length) && (lines[i].IndexOf("--") != NotFound))
            if (i >= lines.Length)
            {
                return;
            }

            var Serial = lines[i].IndexOf("Serial") > NotFound;
            var Phy = lines[i].IndexOf("Phy") > NotFound;
            var Encl_Slot = lines[i].IndexOf("Encl-Slot") > NotFound;
            var Model = lines[i].IndexOf("Model") > NotFound;
            i += 2;

            while (i < lines.Length && lines[i].IndexOf("BBU") == NotFound)
            {
                values = ElliminateCharsAndCreateArray(lines[i++], ' ');

                if (values.Length <= 4)
                    continue;

                var port = values[0];
                status = values[1];
                var unit = values[2];
                var size = $"{values[3]} {values[4]}";

                if (values.Length == 7 && Serial)
                {
                    var blocks = Convert.ToUInt64(values[5]);
                    var serial = values[6];
                    Ports[j++] = new Port(CtlID, port, status, unit, size, blocks, serial);
                }
                else if (values.Length > 8 && Phy && Encl_Slot && Model)
                {
                    var type = values[5];
                    var phy = values[6];
                    var encSlot = values[7];
                    var model = new StringBuilder();
                    for (var k = 8; k < values.Length; k++)
                    {
                        if (k > 8) model.Append(' ');
                        model.Append(values[k]);
                    }
                    Ports[j++] = new Port(CtlID, port, status, unit, size, type, phy, encSlot, model.ToString());
                }
                else
                {
                    Ports[j++] = new Port(CtlID, port, status, unit, size);
                }
            }
            i++;

            if (i >= lines.Length)
                return;

            values = ElliminateCharsAndCreateArray(lines[i], ' ');

            if (values.Length != 8)
                return;

            var name = values[0];
            var onlineState = Convert.ToBoolean(values[1]);
            Decide? bbu_ready = null;
            switch (values[2].ToUpper())
            {
                case Yes:
                    bbu_ready = Decide.Yes;
                    break;
                case No:
                    bbu_ready = Decide.No;
                    break;
            }
            status = values[3];
            var volt = values[4];
            var temp = values[5];
            var hours = values[6];
            var lastCapTest = Convert.ToDateTime(values[7]);
            BBU = new BBU(CtlID, name, onlineState, bbu_ready, status, volt, temp, hours, lastCapTest);
        }
    }
}