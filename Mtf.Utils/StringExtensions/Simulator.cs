using Mtf.Utils.CharExtensions;

namespace Mtf.Utils.StringExtensions
{
    public static class Simulator
    {
        public static void SimulateKeys(this string value)
        {
            for (var i = 0; i < value.Length; i++)
            {
                value[i].SimulateKeyPress();
            }
        }
    }
}