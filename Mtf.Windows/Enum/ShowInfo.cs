using System;

namespace Enums
{
	[Flags]
	public enum ShowInfo : byte
	{
		None = 0,
		CameraName = 1,
		DateTime = 2,
		CameraNameAndDateTime = CameraName | DateTime
	}
}
