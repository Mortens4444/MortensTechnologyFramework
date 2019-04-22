using System;
using System.Drawing;
using Mtf.Graphics.Enum;
using Mtf.Graphics.Types;
using Mtf.Utils.IntExtensions;

namespace Mtf.Graphics.ColorExtensions
{
    public static class ColorTransformExtensions
    {
        public static Color InverseColor(this Color value)
        {
            return Color.FromArgb(value.A, (byte)~value.R, (byte)~value.G, (byte)~value.B);
        }

        public static Color ConvertToBlackOrWhite(this Color value)
        {
            var distance = value.GetBT709Value();
            return distance < 128 ? Color.Black : Color.White;
        }

        public static Color ConvertToSimpleAvarageGrayscale(this Color value)
        {
            int grayValue = (byte)Math.Round((double)(value.R + value.G + value.B) / 3);
            return Color.FromArgb(grayValue, grayValue, grayValue);
        }

        public static Color ConvertToWeightedAvarageGrayscale(this Color value)
        {
            int grayValue = (byte)Math.Round((double)(3 * value.R + 2 * value.G + 4 * value.B) / 9);
            return Color.FromArgb(grayValue, grayValue, grayValue);
        }

        public static Color ConvertToGrayscale_1(this Color value)
        {
            int grayValue = (byte)Math.Round((double)(77 * value.R + 150 * value.G + 28 * value.B) / 255);
            return Color.FromArgb(grayValue, grayValue, grayValue);
        }

        public static Color ConvertToGrayscale_2(this Color value)
        {
            int grayValue = (byte)Math.Round((double)(222 * value.R + 707 * value.G + 71 * value.B) / 1000);
            return Color.FromArgb(grayValue, grayValue, grayValue);
        }

        public static Color ConvertToBT_601_Grayscale(this Color value)
        {
            var grayValue = (int)Math.Round(value.GetBT601Value()); // NTSC, PAL
            return Color.FromArgb(grayValue, grayValue, grayValue);
        }

        public static Color ConvertToGrayscale_4(this Color value)
        {
            int grayValue = (byte)Math.Round(0.197 * value.R + 0.701 * value.G + 0.07 * value.B);
            return Color.FromArgb(grayValue, grayValue, grayValue);
        }

        public static Color ConvertToBT_609_Grayscale(this Color value)
        {
            int grayValue = (byte)Math.Round(0.21 * value.R + 0.71 * value.G + 0.08 * value.B);
            return Color.FromArgb(grayValue, grayValue, grayValue);
        }

        public static Color ConvertToBT_709_Grayscale(this Color value)
        {
            int grayValue = (byte)Math.Round(value.GetBT709Value());
            return Color.FromArgb(grayValue, grayValue, grayValue);
        }

        public static Color ConvertToRMY_Grayscale(this Color value)
        {
            int grayValue = (byte)Math.Round(((0.5 * value.R) + (0.419 * value.G)) + (0.081 * value.B));
            return Color.FromArgb(grayValue, grayValue, grayValue);
        }

        public static Color ConvertToRedscale(this Color value)
        {
            return Color.FromArgb(value.R, 0, 0);
        }

        public static Color ConvertToGreenscale(this Color value)
        {
            return Color.FromArgb(0, value.G, 0);
        }

        public static Color ConvertToBluescale(this Color value)
        {
            return Color.FromArgb(0, 0, value.B);
        }

        public static Color ConvertToRedGreenscale(this Color value)
        {
            return Color.FromArgb(value.R, value.G, 0);
        }

        public static Color ConvertToGreenBluescale(this Color value)
        {
            return Color.FromArgb(0, value.G, value.B);
        }

        public static Color ConvertToRedBluescale(this Color value)
        {
            return Color.FromArgb(value.R, 0, value.B);
        }

        public static YUV_Color ConvertToYUV_Color(this Color value)
        {
            return new YUV_Color(value);
        }

        public static Color ConvertToYUV_Y_scale(this Color value)
        {
            var yuvColor = ConvertToYUV_Color(value);
            return Color.FromArgb(yuvColor.Y, 0, 0);
        }

        public static Color ConvertToCMY_C_scale(this Color value)
        {
            var cmyColor = new CMY_Color(value);
            return Color.FromArgb(cmyColor.C, 0, 0);
        }

        public static Color ConvertToCMY_M_scale(this Color value)
        {
            var cmyColor = new CMY_Color(value);
            return Color.FromArgb(0, cmyColor.M, 0);
        }

        public static Color ConvertToCMY_Y_scale(this Color value)
        {
            var cmyColor = new CMY_Color(value);
            return Color.FromArgb(0, 0, cmyColor.Y);
        }

        public static Color ConvertToCMY_CM_scale(this Color value)
        {
            var cmyColor = new CMY_Color(value);
            return Color.FromArgb(cmyColor.C, cmyColor.M, 0);
        }

        public static Color ConvertToCMY_MY_scale(this Color value)
        {
            var cmyColor = new CMY_Color(value);
            return Color.FromArgb(0, cmyColor.M, cmyColor.Y);
        }

        public static Color ConvertToCMY_CY_scale(this Color value)
        {
            var cmyColor = new CMY_Color(value);
            return Color.FromArgb(cmyColor.C, 0, cmyColor.Y);
        }

        public static Color ConvertToYUV_U_scale(this Color value)
        {
            var yuvColor = ConvertToYUV_Color(value);
            return Color.FromArgb(0, yuvColor.U, 0);
        }

        public static Color ConvertToYUV_V_scale(this Color value)
        {
            var yuvColor = ConvertToYUV_Color(value);
            return Color.FromArgb(0, 0, yuvColor.V);
        }

        public static Color ConvertToYUV_YU_scale(this Color value)
        {
            var yuvColor = ConvertToYUV_Color(value);
            return Color.FromArgb(yuvColor.Y, yuvColor.U, 0);
        }

        public static Color ConvertToYUV_UV_scale(this Color value)
        {
            var yuvColor = ConvertToYUV_Color(value);
            return Color.FromArgb(0, yuvColor.U, yuvColor.V);
        }

        public static Color ConvertToYUV_YV_scale(this Color value)
        {
            var yuvColor = ConvertToYUV_Color(value);
            return Color.FromArgb(yuvColor.Y, 0, yuvColor.V);
        }

        public static Color ConvertToInverse(this Color value)
        {
            return Color.FromArgb(byte.MaxValue - value.R, byte.MaxValue - value.G, byte.MaxValue - value.B);
        }

        public static Color ConvertToInverse_Red(this Color value)
        {
            return Color.FromArgb(byte.MaxValue - value.R, value.G, value.B);
        }

        public static Color ConvertToInverse_Green(this Color value)
        {
            return Color.FromArgb(value.R, byte.MaxValue - value.G, value.B);
        }

        public static Color ConvertToInverse_Blue(this Color value)
        {
            return Color.FromArgb(value.R, value.G, byte.MaxValue - value.B);
        }

        public static Color ConvertToInverse_Red_Blue(this Color value)
        {
            return Color.FromArgb(byte.MaxValue - value.R, value.G, byte.MaxValue - value.B);
        }

        public static Color ConvertToInverse_Green_Blue(this Color value)
        {
            return Color.FromArgb(byte.MaxValue - value.R, byte.MaxValue - value.G, byte.MaxValue - value.B);
        }

        public static Color ConvertToInverse_Red_Green(this Color value)
        {
            return Color.FromArgb(byte.MaxValue - value.R, byte.MaxValue - value.G, byte.MaxValue - value.B);
        }

        public static Color ConvertToRBG(this Color value)
        {
            return Color.FromArgb(value.R, value.B, value.G);
        }

        public static Color ConvertToBGR(this Color value)
        {
            return Color.FromArgb(value.B, value.G, value.R);
        }

        public static Color ConvertToGRB(this Color value)
        {
            return Color.FromArgb(value.G, value.R, value.B);
        }

        public static Color ConvertToGBR(this Color value)
        {
            return Color.FromArgb(value.G, value.B, value.R);
        }

        public static Color ConvertToBRG(this Color value)
        {
            return Color.FromArgb(value.B, value.R, value.G);
        }

        public static Color ConvertFromYUVToRGB(this Color value)
        {
            var yuvColor = new YUV_Color(value.R, value.G, value.B, ColorSpaceType.YUV);
            return Color.FromArgb(yuvColor.R, yuvColor.G, yuvColor.B);
        }

        public static Color ConvertFromCMYToRGB(this Color value)
        {
            var cmyColor = new CMY_Color(value.R, value.G, value.B);
            return Color.FromArgb(cmyColor.R, cmyColor.G, cmyColor.B);
        }

        public static Color ConvertToExp(this Color value)
        {
            return Color.FromArgb(((int)Math.Round(Math.Exp(value.R)) % 255).LimitMe(0, 255), ((int)Math.Round(Math.Exp(value.G)) % 255).LimitMe(0, 255), ((int)Math.Round(Math.Exp(value.B)) % 255).LimitMe(0, 255));
        }

        public static Color ConvertToPow(this Color value)
        {
            return Color.FromArgb(((int)Math.Round(Math.Abs(Math.Exp(value.R))) % 255).LimitMe(0, 255), ((int)Math.Round(Math.Abs(Math.Exp(value.G))) % 255).LimitMe(0, 255), ((int)Math.Round(Math.Abs(Math.Exp(value.B))) % 255).LimitMe(0, 255));
        }
    }
}