using System;
using System.Drawing;
using Mtf.Utils.StringExtensions;

namespace Mtf.Utils.PointExtensions
{
    public class PointUtils
    {
        public static Point ConvertToPoint(string pointToString)
        {
            try
            {
                return new Point(Convert.ToInt32(pointToString.Substring("X=", ",")), Convert.ToInt32(pointToString.Substring("Y=", "}")));
            }
            catch
            {
                return Point.Empty;
            }
        }
    }
}