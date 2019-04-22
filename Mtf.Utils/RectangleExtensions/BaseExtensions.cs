using System.Drawing;

namespace Mtf.Utils.RectangleExtensions
{
    public static class BaseExtensions
    {
        public static Point GetMiddle(this Rectangle value)
        {
            return new Point
            {
                X = value.GetMiddleX(),
                Y = value.GetMiddleY()
            };
        }

        public static int GetMiddleX(this Rectangle value)
        {
            return value.Left + value.Width / 2;
        }

        public static int GetMiddleY(this Rectangle value)
        {
            return value.Top + value.Height / 2;
        }
    }
}