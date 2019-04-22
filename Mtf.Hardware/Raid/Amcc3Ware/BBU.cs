using System;
using Mtf.Hardware.Raid.Amcc3Ware.Enum;
using Mtf.Hardware.Raid.Areca.Enum;
using Decide = Mtf.Hardware.Raid.Amcc3Ware.Enum.Decide;

namespace Mtf.Hardware.Raid.Amcc3Ware
{
    public class BBU : CommandExecutor
    {
        public int CtlID;
        public string Name;
        public bool OnlineState;
        public Decide? BBUReady;
        public string Status;
        public string Volt;
        public string Temp;
        public string Hours;
        public DateTime LastCapTest;

        public BBU(int controllerId, string name, bool onlineState, Decide? bbuReady, string status, string volt, string temp, string hours, DateTime lastCapTest)
        {
            CtlID = controllerId;
            Name = name;
            OnlineState = onlineState;
            BBUReady = bbuReady;
            Status = status;
            Volt = volt;
            Temp = temp;
            Hours = hours;
            LastCapTest = lastCapTest;
        }

        public override string ToString()
        {
            return $"Name {Name}, Hours {Hours}";
        }

        public string EnableOrDisable(EnableDisable state)
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.BBUEnableOrDisable, CtlID, state.ToString().ToLower());
        }

        public string Test(bool quiet)
        {
            TW_CLI_ParameterType parameter;
            if (quiet) parameter = TW_CLI_ParameterType.BBUTestQuiet;
            else parameter = TW_CLI_ParameterType.BBUTest;
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, parameter, CtlID);
        }

        public string Show()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.BBUShow, CtlID);
        }

        public string ShowAll()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.BBUShowAll, CtlID);
        }

        public string ShowBatteryCapacityInHours()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.BBUShowBatteryCapacityInHours, CtlID);
        }

        public string ShowBatteryInstallationDate()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.BBUShowBatteryInstallationDate, CtlID);
        }

        public string ShowBootLoader()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.BBUShowBootLoader, CtlID);
        }

        public string ShowFirmware()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.BBUShowFirmware, CtlID);
        }

        public string ShowLastTest()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.BBUShowLastTest, CtlID);
        }

        public string ShowPCB()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.BBUShowPCB, CtlID);
        }

        public string ShowSerial()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.BBUShowSerial, CtlID);
        }

        public string ShowStatus()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.BBUShowStatus, CtlID);
        }

        public string ShowTemp()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.BBUShowTemp, CtlID);
        }

        public string ShowVolt()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.BBUShowVolt, CtlID);
        }
    }
}