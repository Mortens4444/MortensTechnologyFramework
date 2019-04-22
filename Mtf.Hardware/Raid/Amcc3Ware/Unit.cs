using System;
using System.Security.Permissions;
using Mtf.Hardware.Raid.Amcc3Ware.Enum;
using Mtf.Utils.StringExtensions;

namespace Mtf.Hardware.Raid.Amcc3Ware
{
    public class Unit : SubUnit
    {
        public string Name;
        public string Serial;

        public string Cache;
        //public string VIM;
        public OnOffUnknown AVerify;
        public OnOffUnknown IgnECC;

        public SubUnit[] SubUnits;

        public Unit(int controllerId, string unit, string unitType, string status, string cmpl)
            : base(controllerId, unit, unitType, status, cmpl, null, null, -1, null)
        {
            Name = GetName();
            Serial = GetSerial();
            OK = status == "OK";
        }

        public Unit(int controllerId, string unit, string unitType, string status, string cmpl, string vim, string stripe, double sizeInGb, string cache, OnOffUnknown averify)
            : base(controllerId, unit, unitType, status, cmpl, null, stripe, sizeInGb, null)
        {
            Cache = cache;
            AVerify = averify;
            VIM = vim;

            Name = GetName();
            Serial = GetSerial();
            OK = status == "OK";
        }

        public Unit(int controllerId, string unit, string unitType, string status, string cmpl, string stripe, double sizeInGb, string cache, OnOffUnknown averify, OnOffUnknown ign_ecc)
            : base(controllerId, unit, unitType, status, cmpl, null, stripe, sizeInGb, null)
        {
            Cache = cache;
            AVerify = averify;
            IgnECC = ign_ecc;

            Name = GetName();
            Serial = GetSerial();
            OK = status == "OK";
        }

        public override string ToString()
        {
            if (!String.IsNullOrEmpty(Name))
            {
                return String.Format(base.ToString() + ", Name {0}, Serial {1}, Rebuild status {2}", Name, Serial, GetRebuildStatus());
            }

            return String.Format(base.ToString() + ", Serial {0}, Rebuild status {1}", Serial, GetRebuildStatus());
        }

        public string InformationAboutControllerUnit()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.InfoContollerUnit, CtlID, UnitID);
        }

        public void GetSummaryInformation()
        {
            var output = ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.SummaryInformationAboutTheSpecifiedUnit, CtlID, UnitID);
            SubUnits = GetSummaryInformation(output);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "ExportOrRemoveUnit")]
        public string ExportOrRemove(bool noScan, bool quiet)
        {
            TW_CLI_ParameterType parameter;
            if (noScan && quiet) parameter = TW_CLI_ParameterType.UnitExportNoScanQuiet;
            else
            {
                if (noScan) parameter = TW_CLI_ParameterType.UnitExportNoScan;
                else if (quiet) parameter = TW_CLI_ParameterType.UnitExportQuiet;
                else parameter = TW_CLI_ParameterType.UnitExport;
            }
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, parameter, CtlID, UnitID);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "DeleteUnit")]
        public string Delete(bool noScan, bool quiet)
        {
            TW_CLI_ParameterType parameter;
            if (noScan && quiet) parameter = TW_CLI_ParameterType.UnitDeleteNoScanQuiet;
            else
            {
                if (noScan) parameter = TW_CLI_ParameterType.UnitDeleteNoScan;
                else if (quiet) parameter = TW_CLI_ParameterType.UnitDeleteQuiet;
                else parameter = TW_CLI_ParameterType.UnitDelete;
            }
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, parameter, CtlID, UnitID);
        }

        public string Flush()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.UnitFlush, CtlID, UnitID);
        }

        public string PauseRebuild()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.UnitPauseRebuild, CtlID, UnitID);
        }

        public string ResumeRebuild()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.UnitResumeRebuild, CtlID, UnitID);
        }

        public string SetAutoRecovery(OnOff state)
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.UnitSetAutoRecovery, CtlID, UnitID.ToString(), state.ToString().ToLower());
        }

        public string StartVerify()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.UnitStartVerify, CtlID, UnitID);
        }

        public string StopVerify()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.UnitStopVerify, CtlID, UnitID);
        }

        public string SetIgnoreECC(OnOff state)
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.UnitSetIgnoreECC, CtlID, UnitID.ToString(), state.ToString().ToLower());
        }

        public string SetName(string name)
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.UnitSetName, CtlID, UnitID.ToString(), name);
        }

        public string SetCache(OnOff state, bool quiet)
        {
            var parameter = quiet ? TW_CLI_ParameterType.UnitSetCacheQuiet : TW_CLI_ParameterType.UnitSetCache;
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, parameter, CtlID, UnitID.ToString(), state.ToString().ToLower());
        }

        public string GetUnitStatus()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.ShowUnitStatus, CtlID, UnitID);
        }

        public string GetAll()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.UnitAll, CtlID, UnitID);
        }

        public string GetVolumes()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.UnitVolumes, CtlID, UnitID);
        }

        public string GetInitializeStatus()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.UnitInitializeStatus, CtlID, UnitID);
        }

        public string GetSerial()
        {
            var output = ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.UnitSerial, CtlID, UnitID);
            var lines = output.SplitOnNewLines();

            var index = lines[0].IndexOf(" = ");
            if (index > NotFound)
            {
                return lines[0].Substring(index + 3).Trim();
            }
            return output;
        }

        public string GetName()
        {
            var output = ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.UnitName, CtlID, UnitID);
            var lines = output.SplitOnNewLines();

            var index = lines[0].IndexOf(" = ");
            if (index > NotFound)
            {
                return lines[0].Substring(index + 3).Trim();
            }

            return output;
        }

        public string GetRebuildStatus()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.UnitRebuildStatus, CtlID, UnitID);
        }

        public string GetVerifyStatus()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.UnitVerifyStatus, CtlID, UnitID);
        }

        private SubUnit[] GetSummaryInformation(string output_of___cx_ux_show)
        {
            var lines = output_of___cx_ux_show.SplitOnNewLines();

            var i = 0;
            while (i < lines.Length && lines[i].IndexOf("--") == NotFound) i++;

            var VIM = lines[i - 1].IndexOf("%V/I/M") > NotFound;
            var Blocks = lines[i - 1].IndexOf("Blocks") > NotFound;

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
            var result = new SubUnit[db];

            var j = 0;
            while (j < db)
            {
                var values = ElliminateCharsAndCreateArray(lines[i++], ' ');

                var unit = values[0];
                var unit_type = values[1];
                var status = values[2];
                var cmpl = values[3];

                if (Blocks)
                {
                    var port = values[4];
                    var stripe = values[5];
                    var sizeInGb = Convert.ToDouble(values[6], AMCC_3Ware_State.number_format_info);
                    var blocks = Convert.ToUInt64(values[7]);
                    result[j++] = new SubUnit(CtlID, unit, unit_type, status, cmpl, port, stripe, sizeInGb, blocks);
                }
                else
                {
                    if (VIM)
                    {
                        var vim = values[4];
                        var vport = values[5];
                        var stripe = values[6];
                        var sizeInGb = Convert.ToDouble(values[7], AMCC_3Ware_State.number_format_info);
                        result[j++] = new SubUnit(CtlID, unit, unit_type, status, cmpl, vim, vport, stripe, sizeInGb);
                    }
                }
            }
            return result;
        }
    }
}