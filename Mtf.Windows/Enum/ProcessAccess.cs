﻿using System;

namespace Enums
{
	[Flags]
	public enum ProcessAccess
	{
		PROCESS_ALL_ACCESS = 0x001F0FFF,
		PROCESS_TERMINATE = 0x00000001,
		PROCESS_CREATE_THREAD = 0x00000002,
		PROCESS_VM_OPERATION = 0x00000008,
		PROCESS_VM_READ = 0x00000010,
		PROCESS_VM_WRITE = 0x00000020,
		PROCESS_DUP_HANDLE = 0x00000040,
		PROCESS_SET_INFORMATION = 0x00000200,
		PROCESS_QUERY_INFORMATION = 0x00000400,
		PROCESS_SYNCRONIZE = 0x00100000
	}
}