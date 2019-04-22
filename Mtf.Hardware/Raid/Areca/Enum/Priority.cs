using Mtf.Utils.EnumExtensions;

namespace Mtf.Hardware.Raid.Areca.Enum
{
	public enum Priority : byte
	{
	    [SecondaryValue(3)]
		High = 1,
	    [SecondaryValue(3)]
		AboveNormal = 2,
	    [SecondaryValue(2)]
		Normal = 3,
	    [SecondaryValue(1)]
		BelowNormal = 4,
	    [SecondaryValue(0)]
		Low = 5
	}
}
