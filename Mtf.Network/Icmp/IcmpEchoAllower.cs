/*using System;
using NetFwTypeLib;

namespace Mtf.Network.Icmp
{
    // TODO Fix this class
    public class IcmpEchoAllower
    {
        /// <summary>
        /// Set up Windows Firewall rule to allow Ping requests.
        /// </summary>
        public static void AllowICMPEcho()
        {
            var icmpRule = (INetFwRule)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
            icmpRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
            icmpRule.Description = "Allow ICMP echo request";
            icmpRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
            icmpRule.Enabled = true;
            icmpRule.InterfaceTypes = "All";
            icmpRule.Name = "Ping allow";

            var fw_policy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWPolicy2"));
            fw_policy.Rules.Add(icmpRule);
        }
    }
}*/