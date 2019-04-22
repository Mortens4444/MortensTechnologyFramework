using System;
using System.Security.Permissions;
using Mtf.Hardware.Raid.Amcc3Ware.Enum;
using Mtf.Utils.StringExtensions;

namespace Mtf.Hardware.Raid.Amcc3Ware
{
    public class Port : CommandExecutor
    {
        private const int NotFound = -1;

        public int CtlID;
        public int PortID;
        public string PortName;
        public string Status;
        public string Unit;
        public string Size;
        public string FirmwareVersion;
        public string PowerOnHours;
        public string Temperature;
        public string Capacity;
        public string WWN;
        public string DriveTpye;
        public string InterfaceType;
        public string DrivePorts;
        public string DriveConnections;
        public string ReallocatedSectors;
        public string SpindleSpeed;
        public string LinkSpeedSupported;
        public string LinkSpeed;
        public string QueuingSupported;
        public string QueuingEnabled;
        public string IdentifyStatus;

        public ulong Blocks;
        public string Serial;
        public bool OK;

        public string Type;
        public string Phy;
        public string EncSlot;
        public string Model;

        public Port(int controllerId, string port, string status, string unit, string size)
        {
            CtlID = controllerId;
            PortName = port;
            PortID = Convert.ToInt32(PortName.Substring(1));
            Status = status;
            Unit = unit;
            Size = size;
            OK = Status == "OK";
            GetDetails();
        }

        public Port(int controllerId, string port, string status, string unit, string size, string type, string phy, string encSlot, string model)
            : this(controllerId, port, status, unit, size)
        {
            Type = type;
            Phy = phy;
            EncSlot = encSlot;
            Model = model;
        }

        public Port(int controllerId, string port, string status, string unit, string size, ulong blocks, string serial)
            : this(controllerId, port, status, unit, size)
        {
            Blocks = blocks;
            Serial = serial;
        }

        public override string ToString()
        {
            if (Serial != null)
            {
                return $"PortName {PortName}, Serial {Serial}, Unit {Unit}, Status {Status}, Temp {Temperature}";
            }

            return $"PortName {PortName}, Model {Model}, Unit {Unit}, Status {Status}, Temp {Temperature}";
        }

        public string Show()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.PortShow, CtlID, PortID);
        }

        public string ShowAll()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.PortShowAll, CtlID, PortID);
        }

        public string GetCapacity()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.PortShowCapacity, CtlID, PortID);
        }

        public string GetModel()
        {
            var output = ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.PortShowModel, CtlID, PortID);
            var index = output.IndexOf(" = ");
            return index > -1 ? output.Substring(index + 3) : output;
        }

        public string GetFirmwareVersion()
        {
            var output = ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.PortShowFirmware, CtlID, PortID);
            var index = output.IndexOf(" = ");
            return index > -1 ? output.Substring(index + 3) : output;
        }

        public string GetSerial()
        {
            var output = ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.PortShowSerial, CtlID, PortID);
            var index = output.IndexOf(" = ");
            return index > -1 ? output.Substring(index + 3) : output;
        }

        public void GetDetails()
        {
            var lines = ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.PortShowAll, CtlID, PortID).SplitOnNewLines();
            foreach (var line in lines)
            {
                int index;

                if ((index = line.IndexOf("Model = ")) > NotFound)
                {
                    Model = line.Substring(index + 8);
                }
                else if ((index = line.IndexOf("Firmware Version = ")) > NotFound)
                {
                    FirmwareVersion = line.Substring(index + 19);
                }
                else if ((index = line.IndexOf("Serial = ")) > NotFound)
                {
                    Serial = line.Substring(index + 9);
                }
                else if ((index = line.IndexOf("Power On Hours = ")) > NotFound)
                {
                    PowerOnHours = line.Substring(index + 17);
                }
                else if ((index = line.IndexOf("Temperature = ")) > NotFound)
                {
                    Temperature = line.Substring(index + 14).Replace(" deg ", "°");
                }
                else if ((index = line.IndexOf("Capacity = ")) > NotFound)
                {
                    Capacity = line.Substring(index + 11);
                }
                else if ((index = line.IndexOf("WWN = ")) > NotFound)
                {
                    WWN = line.Substring(index + 6);
                }
                else if ((index = line.IndexOf("DriveTpye = ")) > NotFound)
                {
                    DriveTpye = line.Substring(index + 12);
                }
                else if ((index = line.IndexOf("InterfaceType = ")) > NotFound)
                {
                    InterfaceType = line.Substring(index + 16);
                }
                else if ((index = line.IndexOf("DrivePorts = ")) > NotFound)
                {
                    DrivePorts = line.Substring(index + 13);
                }
                else if ((index = line.IndexOf("DriveConnections = ")) > NotFound)
                {
                    DriveConnections = line.Substring(index + 19);
                }
                else if ((index = line.IndexOf("ReallocatedSectors = ")) > NotFound)
                {
                    ReallocatedSectors = line.Substring(index + 21);
                }
                else if ((index = line.IndexOf("SpindleSpeed = ")) > NotFound)
                {
                    SpindleSpeed = line.Substring(index + 15);
                }
                else if ((index = line.IndexOf("LinkSpeedSupported = ")) > NotFound)
                {
                    LinkSpeedSupported = line.Substring(index + 21);
                }
                else if ((index = line.IndexOf("LinkSpeed = ")) > NotFound)
                {
                    LinkSpeed = line.Substring(index + 12);
                }
                else if ((index = line.IndexOf("QueuingSupported = ")) > NotFound)
                {
                    QueuingSupported = line.Substring(index + 19);
                }
                else if ((index = line.IndexOf("QueuingEnabled = ")) > NotFound)
                {
                    QueuingEnabled = line.Substring(index + 17);
                }
                else if ((index = line.IndexOf("IdentifyStatus = ")) > NotFound)
                {
                    IdentifyStatus = line.Substring(index + 17);
                }
            }
        }

        public string GetIdentify()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.PortIdentify, CtlID, PortID);
        }

        public string GetLinkSpeed()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.PortLSpeed, CtlID, PortID);
        }

        public string GetSMART()
        {
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.PortShowSMART, CtlID, PortID);
        }

        public string GetStatus()
        {
            var output = ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, TW_CLI_ParameterType.PortShowStatus, CtlID, PortID);
            var index = output.IndexOf(" = ");
            if (index > NotFound)
            {
                return output.Substring(index + 3);
            }
            return output;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "ExportOrRemovePort")]
        public string ExportOrRemove(bool noScan, bool quiet)
        {
            TW_CLI_ParameterType parameter;
            if (noScan && quiet)
            {
                parameter = TW_CLI_ParameterType.PortExportNoScanQuiet;
            }
            else
            {
                if (noScan)
                {
                    parameter = TW_CLI_ParameterType.PortExportNoScan;
                }
                else if (quiet)
                {
                    parameter = TW_CLI_ParameterType.PortExportQuiet;
                }
                else
                {
                    parameter = TW_CLI_ParameterType.PortExport;
                }
            }
            return ExecuteCommand(AMCC_3Ware_State.TW_CLI_path, parameter, CtlID, PortID);
        }
    }

}