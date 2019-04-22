using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;
using Mtf.Hardware.Raid.Areca.Enum;

namespace Mtf.Hardware.Raid.Areca
{
    public class VolumeSet : CommandParser
    {
        public int VolumeSetId { get; }
        public string Name { get; }
        public string RaidSetName { get; }
        public string VolumeCapacity { get; }
        public string ScsiChIdLun { get; }
        public string RaidLevel { get; }
        public string StripeSize { get; }
        public string BlockSize { get; }
        public int MemberDisks { get; }
        public string CacheMode { get; }
        public string TaggedQueuing { get; }
        public string VolumeState { get; }
        public string GuiErrorMessage { get; }

        public VolumeSet(int volumeSetId)
        {
            information = new Dictionary<string, Tuple<string, Type>>
            {
                { "Volume Set Name", new Tuple<string, Type>(nameof(Name), typeof(string)) },
                { "Raid Set Name", new Tuple<string, Type>(nameof(RaidSetName), typeof(string)) },
                { "Volume capacity", new Tuple<string, Type>(nameof(VolumeCapacity), typeof(string)) },
                { "SCSI Ch/Id/Lun", new Tuple<string, Type>(nameof(ScsiChIdLun), typeof(string)) },
                { "Raid_Level", new Tuple<string, Type>(nameof(RaidLevel), typeof(string)) },
                { "Stripe Size", new Tuple<string, Type>(nameof(StripeSize), typeof(string)) },
                { "Block Size", new Tuple<string, Type>(nameof(BlockSize), typeof(string)) },
                { "Member Disks", new Tuple<string, Type>(nameof(MemberDisks), typeof(int)) },
                { "Cache Mode", new Tuple<string, Type>(nameof(CacheMode), typeof(string)) },
                { "Tagged Queuing", new Tuple<string, Type>(nameof(TaggedQueuing), typeof(string)) },
                { "Volume State", new Tuple<string, Type>(nameof(VolumeState), typeof(string)) },
                { "GuiErrMsg", new Tuple<string, Type>(nameof(GuiErrorMessage), typeof(string)) }
            };

            VolumeSetId = volumeSetId;
            ProcessInfo(Info());
        }

        public override string ToString()
        {
            return $"Volume Set Name: {Name}, Raid Set Name: {RaidSetName}, Volume capacity: {VolumeCapacity}, SCSI Ch/Id/Lun: {ScsiChIdLun}, Raid_Level: {RaidLevel}, Stripe Size: {StripeSize}, Block Size: {BlockSize}, Member Disks: {MemberDisks}, Cache Mode: {CacheMode}, Tagged Queuing: {TaggedQueuing}, Volume State: {VolumeState}";
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "VolumeSetStopCheck")]
        public void StopCheck()
        {
            ExcecuteCommand("vsf stopcheck");
        }

        public string Info()
        {
            return Info(VolumeSetId);
        }

        public string Info(int volumeSetId)
        {
            return ExcecuteCommand($"vsf info vol={volumeSetId}");
        }

        public void Check()
        {
            Check(VolumeSetId);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "VolumeSetCheck")]
        public void Check(int volumeSetId)
        {
            ExcecuteCommand($"vsf check vol={volumeSetId}");
        }

        public void Delete()
        {
            Delete(VolumeSetId);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "VolumeSetDelete")]
        public void Delete(int volumeSetId)
        {
            ExcecuteCommand($"vsf delete vol={volumeSetId}");
        }

        public void Modify(double? sizeInGb, RaidLevel? level, int? ch, int? id, int? lun, string name, Decide? tag, Decide? cache, StripeSize? stripe)
        {
            Modify(VolumeSetId, sizeInGb, level, ch, id, lun, name, tag, cache, stripe);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "VolumeSetCreate")]
        public void Modify(int volumeSetId, double? sizeInGb, RaidLevel? level, int? ch, int? id, int? lun, string name, Decide? tag, Decide? cache, StripeSize? stripe)
        {
            var command = new StringBuilder("vsf modify ");
            command.Append("vol=");
            command.Append(volumeSetId.ToString());
            if (sizeInGb.HasValue)
            {
                command.Append(" capacity=");
                command.Append(sizeInGb.Value.ToString());
            }

            if (level.HasValue)
            {
                command.Append(" level=");
                command.Append(((byte)level).ToString());
            }
            AppendIntValue(command, "ch", ch);
            AppendIntValue(command, "id", id);
            AppendIntValue(command, "lun", lun);

            if (name != null)
            {
                command.Append(" name=");
                command.Append(name);
            }
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
            if (stripe.HasValue)
            {
                command.Append(" stripe=");
                command.Append(((byte)stripe).ToString());
            }
            ExcecuteCommand(command.ToString());
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "VolumeSetCreate")]
        public void Create(int raidSetId, double sizeInGb, RaidLevel level, int? ch, int? id, int? lun, string name, Decide? tag, Decide? cache, StripeSize? stripe, Decide? fginit, GreaterThan2TbVolumeSupport? gt2tb)
        {
            var command = new StringBuilder("vsf create ");
            command.Append("raid=");
            command.Append(raidSetId.ToString());
            command.Append(" capacity=");
            command.Append(sizeInGb.ToString());
            command.Append(" level=");
            command.Append(((byte)level).ToString());

            AppendIntValue(command, "ch", ch);
            AppendIntValue(command, "id", id);
            AppendIntValue(command, "lun", lun);
            if (name != null)
            {
                command.Append(" name=");
                command.Append(name);
            }
            if (tag != null)
            {
                command.Append(" tag=");
                command.Append(tag == Decide.No ? 'N' : 'Y');
            }
            if (cache != null)
            {
                command.Append(" cache=");
                command.Append(cache == Decide.No ? 'N' : 'Y');
            }
            if (stripe != null)
            {
                command.Append(" stripe=");
                command.Append(((byte)stripe).ToString());
            }
            if (fginit != null)
            {
                command.Append(" fginit=");
                command.Append(fginit == Decide.No ? 'N' : 'Y');
            }
            if (gt2tb != null)
            {
                command.Append(" gt2tb=");
                command.Append(gt2tb == GreaterThan2TbVolumeSupport._4_KByte_for_Windows ? "64BIT" : "WIN");
            }
            ExcecuteCommand(command.ToString());
        }
    }
}