using System;
using System.Globalization;
using System.IO;
using System.Text;
using Mtf.Hardware.Raid.Amcc3Ware.Enum;
using Mtf.Utils.DateExtensions;
using Mtf.Utils.StringExtensions;

namespace Mtf.Hardware.Raid.Amcc3Ware
{
    public class AMCC_3Ware_State : CommandExecutor
    {
        private const int NotFound = -1;
        private const string TwCliExe = "tw_cli.exe";

        public Controller[] Controllers;
        public CLI_and_API_Version Versions;
        public Schedule[] RebuildSchedules;
        public Schedule[] VerifySchedules;
        public SelfTest[] SelfTests;
        public static string TW_CLI_path;
        public static NumberFormatInfo number_format_info;

        public AMCC_3Ware_State()
        {
            number_format_info = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            var directory = IntPtr.Size == 8 ? Environment.GetEnvironmentVariable("ProgramW6432") : Environment.GetEnvironmentVariable("ProgramFiles");
            var results = File.Utils.SearchForFirst(directory, TwCliExe);
            if (results.Count == 0)
            {
                throw new FileNotFoundException($"File not found: {TwCliExe}", TwCliExe);
            }
            TW_CLI_path = results[0];

            GetGeneralSummaryOfAllDetectedControllers();

            if (Controllers != null)
            {
                foreach (var controller in Controllers)
                {
                    controller.GetUnitsPortsAndBBU();
                    controller.GetAttributes();

                    //controller.RescanAllPortsAndReconstituteAllUnits();
                    //controller.RescanAllPortsAndReconstituteAllUnitsDoNotInformOS();

                    controller.GetAlarmMessages();
                    controller.GetCurrentRebuildBackgroundTaskSchedules();
                    controller.GetCurrentVerifyBackgroundTaskSchedules();
                    controller.GetSelfTests();

                    foreach (var unit in controller.Units)
                    {
                        unit.GetSummaryInformation();
                    }
                }
            }

            GetCLI_and_API_version();
            GetAllRebuildSchedules();
            GetSelfTests();
        }

        public string GetLogs()
        {
            var log = new StringBuilder();
            foreach (var controller in Controllers)
            {
                log.AppendLine(controller.ToString());
                log.AppendLine();

                log.AppendLine(controller.Diagnostics());

                foreach (var alarm in controller.Alarms)
                {
                    log.AppendLine(alarm.ToString());
                }

                log.AppendLine();
                log.AppendLine();
            }
            return log.ToString();
        }

        public string GetAlarms()
        {
            var log = new StringBuilder();

            log.AppendLine("Alarms");
            log.AppendLine("------");
            for (var i = 0; i < Controllers.Length; i++)
            {
                log.AppendLine("Controller");
                log.AppendLine("----------");
                log.AppendLine(Controllers[i].ToString());

                for (var j = 0; j < Controllers[i].Alarms.Length; i++)
                {
                    log.AppendLine(Controllers[i].Alarms[j].ToString());
                }
            }
            return log.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != GetType())
            {
                return false;
            }

            return obj.ToString() == ToString();
        }

        public override string ToString()
        {
            var toString = new StringBuilder();
            if (Versions != null)
            {
                toString.Append($"Version information: {Versions}");
                toString.AppendLine(Environment.NewLine);
            }

            foreach (var controller in Controllers)
            {
                toString.AppendLine("Controller");
                toString.AppendLine("----------");
                toString.AppendLine(controller.ToString());

                if (controller.Alarms != null)
                {
                    toString.AppendLine();
                    toString.AppendLine("Alarms");
                    toString.AppendLine("------");
                    foreach (var alarm in controller.Alarms)
                    {
                        toString.AppendLine(alarm.ToString());
                    }
                }
                toString.AppendLine();

                if (controller.Units != null)
                {
                    toString.AppendLine("Units");
                    toString.AppendLine("-----");
                    foreach (var unit in controller.Units)
                    {
                        toString.AppendLine($"\t{unit}");
                    }
                }
                if (controller.Ports != null)
                {
                    toString.AppendLine("Ports");
                    toString.AppendLine("-----");
                    foreach (var port in controller.Ports)
                    {
                        toString.AppendLine($"\t{port}");
                    }
                }
                if (controller.BBU != null)
                {
                    toString.AppendLine("BBU");
                    toString.AppendLine("---");
                    toString.AppendLine($"\t{controller.BBU}");
                }
            }
            return toString.ToString();
        }

        public string InformationAboutDetectedControllers()
        {
            return ExecuteCommand(TW_CLI_path, TW_CLI_ParameterType.Info);
        }

        public void GetSelfTests()
        {
            var output = ExecuteCommand(TW_CLI_path, TW_CLI_ParameterType.AllSelfTestSchedulesForThe9000Controllers);
            SelfTests = GetSelfTests(output);
        }

        public void GetAllRebuildSchedules()
        {
            var output = ExecuteCommand(TW_CLI_path, TW_CLI_ParameterType.AllVerifySchedulesForThe9000Controllers);
            VerifySchedules = GetSchedules(output);
        }

        public void GetAllRebuildSchedulesForThe9000Controllers()
        {
            var output = ExecuteCommand(TW_CLI_path, TW_CLI_ParameterType.AllRebuildSchedulesForThe9000Controllers);
            RebuildSchedules = GetSchedules(output);
        }

        public void GetCLI_and_API_version()
        {
            var output = ExecuteCommand(TW_CLI_path, TW_CLI_ParameterType.CLI_and_API_version);
            Versions = GetCLI_and_API_Version(output);
        }

        public void GetGeneralSummaryOfAllDetectedControllers()
        {
            var output = ExecuteCommand(TW_CLI_path, TW_CLI_ParameterType.GeneralSummaryOfAllDetectedControllers);
            Controllers = GetControllers(output);
        }

        private static Controller[] GetControllers(string output_of___show)
        {
            if (output_of___show == null)
            {
                return null;
            }
            var lines = output_of___show.SplitOnNewLines();

            var i = 0;
            while (i < lines.Length && lines[i].IndexOf("--") == NotFound)
            {
                i++;
            }
            i++;

            int db = 0, si = i;
            while (i < lines.Length && lines[i] != String.Empty)
            {
                db++;
                i++;
            }
            i = si;
            var result = new Controller[db];

            var j = 0;
            while (j < db)
            {
                var values = ElliminateCharsAndCreateArray(lines[i++], ' ');

                if (values.Length != 9)
                {
                    continue;
                }

                var name = values[0];
                var model = values[1];
                var ports = Convert.ToInt32(values[2]);
                var drives = Convert.ToInt32(values[3]);
                var units = Convert.ToInt32(values[4]);
                var not_opt = Convert.ToInt32(values[5]);
                var r_rate = Convert.ToInt32(values[6]);
                int? v_rate = null;
                if (values[7] != "-") v_rate = Convert.ToInt32(values[7]);
                var bbu = values[8];

                result[j++] = new Controller(name, model, ports, drives, units, not_opt, r_rate, v_rate, bbu);
            }

            while (i < lines.Length && lines[i].IndexOf("--") == NotFound)
            {
                i++;
            }
            i++;

            db = 0;
            while (i < lines.Length && lines[i] != String.Empty)
            {
                db++;
                i++;
            }
            i -= db;

            j = 0;
            while (j < db)
            {
                var values = ElliminateCharsAndCreateArray(lines[i++], ' ');

                if (values.Length == 7)
                {
                    var name = values[0];
                    var slots = Convert.ToInt32(values[1]);
                    var drives = Convert.ToInt32(values[2]);
                    var fans = Convert.ToInt32(values[3]);
                    var ts_units = Convert.ToInt32(values[4]);
                    var ps_units = Convert.ToInt32(values[5]);
                    var alarms = Convert.ToInt32(values[6]);

                    var index = name.IndexOf('/', 1);
                    var CtlId = Convert.ToInt32(name.Substring(2, index - 2));
                    result[CtlId].AddEnclosure(CtlId, name, slots, drives, fans, ts_units, ps_units, alarms);
                }
                j++;
            }
            return result;
        }

        private static CLI_and_API_Version GetCLI_and_API_Version(string output_of___show_ver)
        {
            var lines = output_of___show_ver.SplitOnNewLines();

            var result = new CLI_and_API_Version
            {
                CLI_version = lines[0].Substring(lines[0].IndexOf(" = ") + 3),
                API_version = lines[1].Substring(lines[0].IndexOf(" = ") + 3)
            };
            return result;
        }

        public static Schedule[] GetSchedules(string output_of___show_rebuild___show_verify___cx_show_rebuild)
        {
            var lines = output_of___show_rebuild___show_verify___cx_show_rebuild.SplitOnNewLines();

            var i = 0;
            while (i < lines.Length && lines[i].IndexOf("--") == NotFound)
            {
                i++;
            }
            i++;

            int db = 0, si = i;
            while (i < lines.Length)
            {
                if (lines[i] != String.Empty) db++;
                i++;
            }
            i = si;

            var result = new Schedule[db];

            var j = 0;
            while (j < db)
            {
                var values = ElliminateCharsAndCreateArray(lines[i++], '\t');
                if (values.Length <= 3)
                    continue;

                var slot = Convert.ToInt32(values[0]);
                var day = new DayConverter().GetDayFromString(values[1]);
                var hour = values[2].ConvertToTimeSpan();

                if (values.Length > 4)
                {
                    var duration = values[3].ConvertToTimeSpan();
                    var status = values[4];
                    result[j++] = new Schedule(slot, day, hour, duration, status);
                }
                else
                {
                    var status = values[3];
                    result[j++] = new Schedule(slot, day, hour, null, status);
                }
            }
            return result;
        }

        public static SelfTest[] GetSelfTests(string output_of___show_selftest)
        {
            var lines = output_of___show_selftest.SplitOnNewLines();

            var i = 0;
            while (i < lines.Length && lines[i].IndexOf("--") == NotFound)
            {
                i++;
            }
            i++;

            int db = 0, si = i;
            while (i < lines.Length)
            {
                if (lines[i] != String.Empty) db++;
                i++;
            }
            i = si;
            var result = new SelfTest[db];

            var j = 0;
            while (j < db)
            {
                var values = ElliminateCharsAndCreateArray(lines[i++], '\t');
                var slot = Convert.ToInt32(values[0]);
                var day = new DayConverter().GetDayFromString(values[1]);
                var hour = values[2].ConvertToTimeSpan();
                var UDMA = values[3];
                string SMART = null;
                if (values.Length > 4)
                {
                    SMART = values[4];
                }
                result[j++] = new SelfTest(slot, day.Value, hour.Value, UDMA, SMART);
            }
            return result;
        }
    }
}