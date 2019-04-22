using System;

namespace Mtf.Utils.DateExtensions
{
    public static class Convertions
    {
        /// <summary>
        /// Prefered to string format
        /// </summary>
        /// <param name="date">The date to represent in string format</param>
        /// <returns>String representation of the date. For example: 2012.11.15 14:02</returns>
        public static string ToStringInPreferedFormat(this DateTime date)
        {
            return $"{date.ToShortDateString()} {date.ToLongTimeString()}";
        }

        /// <summary>
        /// Prefered to string format
        /// </summary>
        /// <param name="date">The date to represent in string format</param>
        /// <returns>String representation of the date. For example: 2012.11.15 14:02:35</returns>
        public static string ToStringInPreferedFormatWithSeconds(this DateTime date)
        {
            return $"{date.ToShortDateString()} {date.ToLongTimeString()}:{date.Second:D2}";
        }

        public static SYSTEMTIME ToSystemTime(this DateTime date)
        {
            return new SYSTEMTIME
            {
                wYear = Convert.ToInt16(date.Year),
                wMonth = Convert.ToInt16(date.Month),
                wDay = Convert.ToInt16(date.Day),
                wHour = Convert.ToInt16(date.Hour),
                wMinute = Convert.ToInt16(date.Minute),
                wSecond = Convert.ToInt16(date.Second),
                wMilliseconds = Convert.ToInt16(date.Millisecond)
            };
        }

        public static DateTime ToDateTime(this SYSTEMTIME systemDate)
        {
            return new DateTime(systemDate.wYear, systemDate.wMonth, systemDate.wDay, systemDate.wHour, systemDate.wMinute, systemDate.wSecond, systemDate.wMilliseconds);
        }
    }
}