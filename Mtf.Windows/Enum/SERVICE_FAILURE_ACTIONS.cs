using System;
using System.Runtime.InteropServices;

namespace Enums
{
	[StructLayout(LayoutKind.Sequential)]
	public struct SERVICE_FAILURE_ACTIONS
	{
		public int dwResetPeriod;
		[MarshalAs(UnmanagedType.LPTStr)]
		public string lpRebootMsg;
		[MarshalAs(UnmanagedType.LPTStr)]
		public string lpCommand;
		public int cActions;
		public IntPtr lpsaActions;
	}
}
