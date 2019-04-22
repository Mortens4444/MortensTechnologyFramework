using System.ComponentModel;

namespace Mtf.Network
{
	public enum MIB_States
	{
	    [Description("CLOSED")]
		MIB_TCP_STATE_CLOSED = 1,
	    [Description("LISTEN")]
		MIB_TCP_STATE_LISTEN = 2,
	    [Description("SYN_SENT")]
		MIB_TCP_STATE_SYN_SENT = 3,
	    [Description("SYN_RCVD")]
		MIB_TCP_STATE_SYN_RCVD = 4,
	    [Description("ESTAB")]
		MIB_TCP_STATE_ESTAB = 5,
	    [Description("FIN_WAIT1")]
		MIB_TCP_STATE_FIN_WAIT1 = 6,
	    [Description("FIN_WAIT2")]
		MIB_TCP_STATE_FIN_WAIT2 = 7,
	    [Description("CLOSE_WAIT")]
		MIB_TCP_STATE_CLOSE_WAIT = 8,
	    [Description("CLOSING")]
		MIB_TCP_STATE_CLOSING = 9,
	    [Description("LAST_ACK")]
		MIB_TCP_STATE_LAST_ACK = 10,
	    [Description("TIME_WAIT")]
		MIB_TCP_STATE_TIME_WAIT = 11,
	    [Description("DELETE_TCB")]
		MIB_TCP_STATE_DELETE_TCB = 12
	}
}
