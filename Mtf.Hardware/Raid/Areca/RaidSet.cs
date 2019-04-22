using System;
using System.Collections.Generic;
using System.Text;

namespace Mtf.Hardware.Raid.Areca
{
    public class RaidSet : CommandParser
    {
        public int RaidSetId { get; }
        public string DiskChannels { get; }
        public string Name { get; private set; }
        public int NumberOfDisks { get; private set; }
        public string TotalCapacity { get; private set; }
        public string FreeCapacity { get; private set; }
        public string MinimumMemberDiskSize { get; private set; }
        public string State { get; private set; }
        public string GuiErrorMessage { get; private set; }
        public List<VolumeSet> VolumeSets { get; } = new List<VolumeSet>();

        private new readonly Dictionary<string, Tuple<string, Type>> information = new Dictionary<string, Tuple<string, Type>>
        {
            { "Raid Set Name", new Tuple<string, Type>(nameof(Name), typeof(string)) },
            { "Member Disks", new Tuple<string, Type>(nameof(NumberOfDisks), typeof(int)) },
            { "Total Raw Capacity", new Tuple<string, Type>(nameof(TotalCapacity), typeof(string)) },
            { "Free Raw Capacity", new Tuple<string, Type>(nameof(FreeCapacity), typeof(string)) },
            { "Min Member Disk Size", new Tuple<string, Type>(nameof(MinimumMemberDiskSize), typeof(string)) },
            { "Raid Set State", new Tuple<string, Type>(nameof(State), typeof(string)) },
            { "GuiErrMsg", new Tuple<string, Type>(nameof(GuiErrorMessage), typeof(string)) }
        };

        public RaidSet(int raidSetId, string diskChannels)
        {
            RaidSetId = raidSetId;
            DiskChannels = diskChannels;
            ProcessInfo(Info());
        }

        public override string ToString()
        {
            var toString = new StringBuilder();
            toString.AppendLine($"ID: {RaidSetId}, Name: {Name}, Number of disks: {NumberOfDisks}, Total capacity: {TotalCapacity}, Free capcity: {FreeCapacity}, Disk channels: {DiskChannels}, Minimum member disk size: {MinimumMemberDiskSize}, State: {State}");
            if (VolumeSets.Count > 0)
            {
                toString.AppendLine("VolumeSets");
                foreach (var volumeSet in VolumeSets)
                {
                    toString.AppendLine(volumeSet.ToString());
                }
            }

            return toString.ToString();
        }

        public void AddVolume(int volumeSetId)
        {
            VolumeSets.Add(new VolumeSet(volumeSetId));
        }

        public void Create(int[] drives, string name)
        {
            var drivesStr = new StringBuilder();
            for (var i = 0; i < drives.Length; i++)
            {
                drivesStr.Append(drives[i].ToString());
                if (i < drives.Length - 1) drivesStr.Append(',');
            }
            ExcecuteCommand($"rsf create drv={drivesStr} name={name}");
        }

        public void Delete()
        {
            Delete(RaidSetId);
        }

        public void Delete(int raidSetId)
        {
            ExcecuteCommand($"rsf delete raid={raidSetId}");
        }

        public void Expand(int raidSetId, int driveId)
        {
            Expand(raidSetId, driveId, null, null);
        }

        public void Expand(int driveId)
        {
            Expand(RaidSetId, driveId, null, null);
        }

        public void Expand(int driveId, int? volume, int? newlevel)
        {
            Expand(RaidSetId, driveId, volume, newlevel);
        }

        public void Expand(int raidSetId, int driveId, int? volume, int? newlevel)
        {
            if (volume != null && newlevel != null)
            {
                ExcecuteCommand($"rsf expand raid={raidSetId} drv={driveId} vol={volume} newlevel={newlevel}");
            }
            else
            {
                ExcecuteCommand($"rsf expand raid={raidSetId} drv={driveId}");
            }
        }

        public void Activate()
        {
            Activate(RaidSetId);
        }

        public void Activate(int raidSetId)
        {
            ExcecuteCommand($"rsf activate raid={raidSetId}");
        }

        public void SetDiskToHotSpareDisk(int driveId)
        {
            ExcecuteCommand($"rsf createhs drv={driveId}");
        }

        public void DeleteHotSpareDisk(int driveId)
        {
            ExcecuteCommand($"rsf deletehs drv={driveId}");
        }

        public string Info()
        {
            return Info(RaidSetId);
        }

        public string Info(int raidSetId)
        {
            return ExcecuteCommand($"rsf info raid={raidSetId}");
        }
    }
}