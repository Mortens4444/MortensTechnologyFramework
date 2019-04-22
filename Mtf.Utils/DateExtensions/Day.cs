using System.ComponentModel;

namespace Mtf.Utils.DateExtensions
{
	public enum Day : byte
	{
	    [Description("")]
		Unknown = 0,
	    [Description("mon")]
		Monday = 1,
	    [Description("tue")]
		Tuesday = 2,
	    [Description("wed")]
		Wednesday = 3,
	    [Description("thu")]
		Thursday = 4,
	    [Description("fri")]
		Friday = 5,
	    [Description("sat")]
		Saturday = 6,
	    [Description("sun")]
		Sunday = 7
	}
}
