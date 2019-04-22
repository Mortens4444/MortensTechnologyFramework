using System;

namespace Enums
{
	[Flags]
	public enum SnapshotFlags
	{
		HeapList = 1,
		Process = 2,
		Thread = 4,
		Module = 8,
		Module32 = 16,
		All = (HeapList | Process | Thread | Module),
		Inherit = -1
	}
}
