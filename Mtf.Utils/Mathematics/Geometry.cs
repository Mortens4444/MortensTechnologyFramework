using System;
using System.Drawing;

namespace Mtf.Utils.Mathematics
{
    public static class Geometry
    {
        public static float RadianToDegree(double radian)
        {
            return (float)(radian * 180 / Math.PI);
        }

        public static float DegreeToRadian(double degree)
        {
            //return 0.01745329251994329576923690768489 * degree;
            return (float)(degree * Math.PI / 180);
        }

        public static float GetAngleBetweenPoints(Point pt1, Point pt2)
        {
            double a = pt2.X - pt1.X;
            double b = pt2.Y - pt1.Y;

            var degree = RadianToDegree(Math.Atan(b / a));

            if (a < 0 && b < 0)
            {
                return degree;
            }
            if (a >= 0)
            {
                return 180 + degree;
            }
            return 360 + degree;
        }
    }
}