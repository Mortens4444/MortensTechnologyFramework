using System;
using System.Collections.Generic;
using System.Text;
using Mtf.Hardware.Raid.Areca.Enum;

namespace Mtf.Hardware.Raid.Areca
{
    public class Disk : CommandParser
    {
        public int DiskId { get; }
        public int IdeChannel { get; private set; }
        public string ModelName { get; private set; }
        public string SerialNumber { get; private set; }
        public string FirmwareRev { get; private set; }
        public double DiskCapacity { get; private set; }
        public string DeviceState { get; private set; }
        public int TimeoutCount { get; private set; }
        public int MediaErrorCount { get; private set; }
        public string GuiErrorMessage { get; private set; }

        public Disk(int diskId)
        {
            information = new Dictionary<string, Tuple<string, Type>>
            {
                { "Model Name", new Tuple<string, Type>(nameof(ModelName), typeof(string)) },
                { "Serial Number", new Tuple<string, Type>(nameof(SerialNumber), typeof(string)) },
                { "Firmware Rev.", new Tuple<string, Type>(nameof(FirmwareRev), typeof(string)) },
                { "Device State", new Tuple<string, Type>(nameof(DeviceState), typeof(string)) },
                { "GuiErrMsg", new Tuple<string, Type>(nameof(GuiErrorMessage), typeof(string)) },
                { "IDE Channel", new Tuple<string, Type>(nameof(IdeChannel), typeof(int)) },
                { "Timeout Count", new Tuple<string, Type>(nameof(TimeoutCount), typeof(int)) },
                { "Media Error Count", new Tuple<string, Type>(nameof(MediaErrorCount), typeof(int)) },
                { "Disk Capacity", new Tuple<string, Type>(nameof(DiskCapacity), typeof(double)) }
            };

            DiskId = diskId;
            ProcessInfo(Info());
        }

        public override string ToString()
        {
            return $"IDE Channel: {IdeChannel}, Model Name: {ModelName}, Serial Number: {SerialNumber}, Firmware Rev.: {FirmwareRev}, Disk Capacity: {DiskCapacity}, Device State: {DeviceState}, Timeout Count: {TimeoutCount}, Media Error Count: {MediaErrorCount}";
        }

        public void Create(int drv, int? ch, int? id, int? lun, Decide? tag, Decide? cache, int? speed)
        {
            var command = CreateCommand("disk create ", drv, ch, id, lun, tag, cache);
            if (speed.HasValue)
            {
                command.Append($" speed={speed.Value}");
            }
            ExcecuteCommand(command.ToString());
        }

        private StringBuilder CreateCommand(string commandBase, int drv, int? ch, int? id, int? lun, Decide? tag, Decide? cache)
        {
            var command = new StringBuilder(commandBase);
            command.Append("drv=");
            command.Append(drv.ToString());
            AppendIntValue(command, "ch", ch);
            AppendIntValue(command, "id", id);
            AppendIntValue(command, "lun", lun);

            if (tag.HasValue)
            {
                command.Append(" tag=");
                command.Append(tag == Decide.No ? 'N' : 'Y');
            }
            if (cache.HasValue)
            {
                command.Append(" cache=");
                command.Append(cache == Decide.No ? 'N' : 'Y');
            }
            return command;
        }

        public void Delete()
        {
            Delete(DiskId);
        }

        public void Delete(int drv)
        {
            ExcecuteCommand($"disk delete drv={drv}");
        }

        public void Modify(int? ch, int? id, int? lun, Decide? tag, Decide? cache)
        {
            Modify(DiskId, ch, id, lun, tag, cache);
        }

        public void Modify(int drv, int? ch, int? id, int? lun, Decide? tag, Decide? cache)
        {
            var command = CreateCommand("disk modify ", drv, ch, id, lun, tag, cache);
            ExcecuteCommand(command.ToString());
        }

        public string DisplayDiskSMART_Data()
        {
            return DisplayDiskSMART_Data(DiskId);
        }

        public string DisplayDiskSMART_Data(int drv)
        {
            return ExcecuteCommand($"disk smart drv={drv}");
        }

        public string Info()
        {
            return Info(DiskId);
        }

        public string Info(int drv)
        {
            return ExcecuteCommand($"disk info drv={drv}");
        }
    }
}