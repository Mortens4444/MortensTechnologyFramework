using System.Drawing;

namespace Mtf.Graphics
{
    public class LocationAndSize
    {
        public LocationAndSize(int x, int y, int width, int height)
            : this(new Point(x, y), new Size(width, height))
        { }

        public LocationAndSize(Point location, Size size)
        {
            Location = location;
            Size = size;
        }

        public Point Location { get; set; }

        public Size Size { get; set; }

        public override string ToString()
        {
            return $"new LocationAndSize(new Point({Location.X}, {Location.Y}), new Size({Size.Width}, {Size.Height}))";
        }
    }
}