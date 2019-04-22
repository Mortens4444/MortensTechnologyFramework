using System.ComponentModel;

namespace Mtf.Network.Ftp
{
    public enum TextInterpretation
    {
        [Description("N")]
        NonPrint,
        [Description("T")]
        TelnetFormatEffectors,
        [Description("C")]
        CarriageControl
    }
}