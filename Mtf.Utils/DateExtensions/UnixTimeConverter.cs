using System;

namespace Mtf.Utils.DateExtensions
{
    public class UnixTimeConverter
    {
        public DateTime ConvertSecondsToDateTime(double seconds)
        {
            return new DateTime(1970, 1, 1).AddSeconds(seconds);
        }
    }
}