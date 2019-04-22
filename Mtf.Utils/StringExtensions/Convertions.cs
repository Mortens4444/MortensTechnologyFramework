using System;
using System.Text;

namespace Mtf.Utils.StringExtensions
{
    public static class Convertions
    {
        private const int NotFound = -1;

        public static byte[] ToByteArray(this string value)
        {
            var result = new byte[value.Length];
            for (var i = 0; i < value.Length; i++)
            {
                int ch = value[i];
                switch (ch)
                {
                    case 336: // Ő
                        result[i] = 212; // Ô
                        break;
                    case 337: // ő
                        result[i] = 244; // ô
                        break;
                    case 368: // Ű
                        result[i] = 219; // Û
                        break;
                    case 369: // ű
                        result[i] = 251; // û
                        break;
                    default:
                        if (ch > Byte.MaxValue) result[i] = 63; // ?
                        else result[i] = Convert.ToByte(value[i]);
                        break;
                }
            }
            return result;
        }

        public static string ToLower(string value)
        {
            return value.ToLower();
        }

        public static string ToUpper(string value)
        {
            return value.ToUpper();
        }

        public static string ToName(string value)
        {
            var result = new StringBuilder();
            result.Append(Char.ToUpper(value.FirstChar()));
            result.Append(value.Substring(1).ToLower());
            return result.ToString();
        }

        public static bool ToBool(this string value)
        {
            return Convert.ToBoolean(value);
        }

        public static byte ToByte(this string value)
        {
            return Convert.ToByte(value);
        }

        public static char ToChar(this string value)
        {
            return Convert.ToChar(value);
        }

        public static DateTime ToDateTime(this string value)
        {
            return Convert.ToDateTime(value);
        }

        public static int ToInt(this string value)
        {
            return Convert.ToInt32(value);
        }

        public static Int64 ToInt64(this string value)
        {
            return Convert.ToInt64(value);
        }

        /// <summary>
        /// Returns a TimeSpan
        /// </summary>
        /// <param name="value">Example 12:00am, or 24 hr(s)</param>
        /// <returns>TimeSpan</returns>
        public static TimeSpan? ConvertToTimeSpan(this string value)
        {
            int days = 0, hours, minutes = 0, seconds = 0, milliseconds = 0;
            value = value.ToLower();
            if (value.IndexOf(':') > NotFound)
            {
                var pm = value.IndexOf("pm") > NotFound;
                value = value.Replace("am", String.Empty).Replace("pm", String.Empty);

                var parts = value.Split('.');
                if (parts.Length > 1)
                {
                    days = Convert.ToInt32(parts[0]);
                    value = parts[1];
                }
                if (parts.Length > 2)
                {
                    milliseconds = Convert.ToInt32(parts[2].Substring(0, 3));
                }
                if (parts.Length > 3)
                {
                    throw new NotImplementedException();
                }

                parts = value.Split(':');
                switch (parts.Length)
                {
                    case 3:
                        seconds = Convert.ToInt32(parts[2]);
                        goto case 2;
                    case 2:
                        minutes = Convert.ToInt32(parts[1]);
                        hours = Convert.ToInt32(parts[0]);
                        if (pm) hours += 12;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            else
            {
                if (value.IndexOf("hr(s)") > NotFound || value.IndexOf("hours") > NotFound)
                {
                    hours = Convert.ToInt32(value.Substring(0, 2));
                }
                else
                {
                    return null;
                }
            }
            return new TimeSpan(days, hours, minutes, seconds, milliseconds);
        }
    }
}