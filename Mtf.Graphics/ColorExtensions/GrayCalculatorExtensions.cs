using System.Drawing;

namespace Mtf.Graphics.ColorExtensions
{
    public static class GrayCalculatorExtensions
    {
        public static double GetBT601Value(this Color value)
        {
            return GetBT601Value(GetNonLinearGammaCorrectedValue(value.R), GetNonLinearGammaCorrectedValue(value.G), GetNonLinearGammaCorrectedValue(value.B));
        }

        public static double GetBT709Value(this Color value)
        {
            return GetBT709Value(GetNonLinearGammaCorrectedValue(value.R), GetNonLinearGammaCorrectedValue(value.G), GetNonLinearGammaCorrectedValue(value.B));
        }

        public static double GetNonLinearGammaCorrectedValue(int component)
        {
            return (double)component / 255;
        }

        public static double GetBT601Value(double red, double green, double blue)
        {
            return 0.299 * red + 0.587 * green + 0.114 * blue;
        }

        public static double GetBT709Value(double red, double green, double blue)
        {
            return 0.2125 * red + 0.7154 * green + 0.0721 * blue;
        }
    }
}