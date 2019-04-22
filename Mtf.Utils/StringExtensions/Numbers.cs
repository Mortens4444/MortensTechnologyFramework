using System;

namespace Mtf.Utils.StringExtensions
{
    public static class Numbers
    {
        public static bool IsNumber(this string value)
        {
            foreach (var ch in value)
            {
                if (!Char.IsDigit(ch))
                {
                    return false;
                }
            }
            return true;
        }
    }
}