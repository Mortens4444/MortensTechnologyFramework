using System.ComponentModel;

namespace Mtf.Network.Packets.Snmp
{
    public enum SnmpMessage
    {
        [Description("1.3.6.1.2.1.1.1.0")]
        SystemInformation,
        [Description("1.3.6.1.2.1.1.2.0")]
        Oids,
        [Description("1.3.6.1.2.1.1.3.0")]
        Uptime,
        [Description("1.3.6.1.2.1.1.4.0")]
        Sysadmin,
        [Description("1.3.6.1.2.1.1.5.0")]
        RemoteHost,
        [Description("1.3.6.1.2.1.1.6.0")]
        ServerRoom,
        [Description("1.3.6.1.2.1.1.8.0")]
        Timeticks,
        [Description("1.3.6.1.6.3.1.1.5.1")]
        ColdStart,
        [Description("1.3.6.1.2.1.1.9.1.2.1")]
        A,
        [Description("1.3.6.1.2.1.1.9.1.2.2")]
        B,
        [Description("1.3.6.1.2.1.1.9.1.2.3")]
        C
    }
}