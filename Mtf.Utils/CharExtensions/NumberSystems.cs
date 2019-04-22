namespace Mtf.Utils.CharExtensions
{
    public static class NumberSystems
    {
        public static bool IsHexadecimalDigit(this char value)
        {
            return value.IsDigit() || value >= 'a' && value <= 'f' || value >= 'A' && value <= 'F';
        }

        public static string CharToHexaRepresentation(this char value)
        {
            return ((int)value).ToString("X");
        }
    }
}