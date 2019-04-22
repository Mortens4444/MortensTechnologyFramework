using System;

namespace Enums
{
	/// <summary>
	/// Service types.
	/// </summary>
	[Flags]
	public enum SERVICE_TYPE : uint
	{
		/// <summary>
		/// Driver service.
		/// </summary>
		SERVICE_KERNEL_DRIVER = 0x00000001,

		/// <summary>
		/// File system driver service.
		/// </summary>
		SERVICE_FILE_SYSTEM_DRIVER = 0x00000002,

		/// <summary>
		/// Service that runs in its own process.
		/// </summary>
		SERVICE_WIN32_OWN_PROCESS = 0x00000010,

		/// <summary>
		/// Service that shares a process with one or more other services.
		/// </summary>
		SERVICE_WIN32_SHARE_PROCESS = 0x00000020,

		/// <summary>
		/// The service can interact with the desktop.
		/// </summary>
		SERVICE_INTERACTIVE_PROCESS = 0x00000100
	}
}
