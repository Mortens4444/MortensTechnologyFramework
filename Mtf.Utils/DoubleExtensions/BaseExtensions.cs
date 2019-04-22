﻿using System;

namespace Mtf.Utils.DoubleExtensions
{
    public static class BaseExtensions
    {
        public static int LimitMeWithRound(this double value, int minimum, int maximum)
        {
            if (maximum < minimum)
            {
                IntExtensions.BaseExtensions.Swap(ref maximum, ref minimum);
            }
            return (int)Math.Round(Math.Max(minimum, Math.Min(maximum, value)));
        }

        public static void Swap(ref double a, ref double b)
        {
            a += b;
            b = a - b;
            a -= b;
        }

        public static int RoundToInt(this double value)
        {
            return (int)Math.Round(value);
        }

        public static int TruncateToInt(this double value)
        {
            return (int)Math.Truncate(value);
        }
    }
}