﻿using System.ComponentModel;

namespace Enums
{
	public enum MonitorType : byte
	{
		[Description("SyncMaster")]
		Samsung_SyncMaster,
		[Description("F-419")]
		Neovo_F419,
		[Description("PL2202W")]
		IIYAMA_PL2202W,
		Unknown
	}
}
