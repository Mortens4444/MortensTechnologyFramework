using System.ComponentModel;

namespace Mtf.Network.Ftp
{
    public enum Structure
    {
        [Description("F")]
        File,
        [Description("R")]
        Record,
        [Description("P")]
        Page
    }
}