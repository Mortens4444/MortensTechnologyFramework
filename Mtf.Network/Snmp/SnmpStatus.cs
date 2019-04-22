namespace Mtf.Network.Snmp
{
	public enum SnmpStatus : byte
	{
		Success = 0,
		MessageError = 1,
		OID_NotExists = 2,
		SetActionNotAvailable = 3,
		GetActionNotAvailable = 4,
		BufferSizeError = 5,
		ValueNotWithinSyntaxError = 6,
		CallbackExecutionError = 7
	}
}
