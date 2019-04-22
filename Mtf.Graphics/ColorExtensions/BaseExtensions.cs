using System;
using System.Drawing;
using Mtf.Graphics.Enum;
using Mtf.Utils;

namespace Mtf.Graphics.ColorExtensions
{
    public static class BaseExtensions
    {
        public static double GetNormalizedValue(ColorComponent component, Color c)
        {
            return GetNormalizedValue(component, c.R, c.G, c.B);
        }

        public static double GetNormalizedValue(ColorComponent component, int red, int green, int blue)
        {
            switch (component)
            {
                case ColorComponent.Red:
                    return (double)red / (red + green + blue);
                case ColorComponent.Green:
                    return (double)green / (red + green + blue);
                case ColorComponent.Blue:
                    return (double)blue / (red + green + blue);
                default:
                    throw new NotImplementedException();
            }
        }

        public static int GetComponentValue(double nonLinearGammaCorrectedValue)
        {
            return (int)Math.Round(nonLinearGammaCorrectedValue * 255);
        }

        public static bool IsColorBetweenColors(this Color value, Color from, Color to)
        {

            int minA = Math.Min(from.A, to.A);
            if (value.A >= minA && value.A <= Math.Abs(from.A - to.A) + minA)
            {
                int min_R = Math.Min(from.R, to.R);
                if (value.R >= min_R && value.R <= Math.Abs(from.R - to.R) + min_R)
                {
                    int min_G = Math.Min(from.G, to.G);
                    if (value.G >= min_G && value.G <= Math.Abs(from.G - to.G) + min_G)
                    {
                        int min_B = Math.Min(from.B, to.B);
                        if (value.B >= min_B && value.B <= Math.Abs(from.B - to.B) + min_B)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static Color GetRandomColor()
        {
            var r = new Random(RandomUtils.GetSeed());
            return Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
        }

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            Color c;
            var h = hue / 60;
            var croma = value * saturation;

            var x = croma * (1 - Math.Abs((h % 2) - 1));

            var hi = Convert.ToInt32(Math.Round(h)) % 6;
            var f = h - Math.Round(h);

            var m = value * (1 - saturation);

            value = value * 255;
            var v = Convert.ToInt32(value);
            var p = Convert.ToInt32(m);
            var q = Convert.ToInt32(value * (1 - f * saturation));
            var t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            switch (hi)
            {
                case 0:
                    c = Color.FromArgb(255, Convert.ToInt32(croma * 255), Convert.ToInt32(x * 255), 0);
                    //c = Color.FromArgb(255, v, t, p);
                    break;
                case 1:
                    c = Color.FromArgb(255, q, v, p);
                    break;
                case 2:
                    c = Color.FromArgb(255, p, v, t);
                    break;
                case 3:
                    c = Color.FromArgb(255, p, q, v);
                    break;
                case 4:
                    c = Color.FromArgb(255, t, p, v);
                    break;
                default:
                    c = Color.FromArgb(255, v, p, q);
                    break;
            }

            var delta = Convert.ToInt32(c.R + m * c.G + m * c.B + m);
            return Color.FromArgb(255, c.R + delta, c.G + delta, c.B + delta);
        }
    }
}