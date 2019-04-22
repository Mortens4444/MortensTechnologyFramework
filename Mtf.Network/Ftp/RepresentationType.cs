using System.ComponentModel;

namespace Mtf.Network.Ftp
{
    public enum RepresentationType
    {
        [Description("A")]
        ASCII,
        [Description("E")]
        EBCDIC,
        [Description("I")]
        Image
     }
}