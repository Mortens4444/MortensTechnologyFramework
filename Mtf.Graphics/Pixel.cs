using System;
using System.Drawing;

namespace Mtf.Graphics
{
    public class Pixel : ICloneable
    {
        public Point Location { get; set; }

        public Color Color { get; set; }

        public int Index { get; set; }

        private readonly int imageWidth;

        public Pixel(int imageWidth, int index, int red, int green, int blue)
        {
            this.imageWidth = imageWidth;
            Index = index;
            Color = Color.FromArgb(red, green, blue);
            Location = IndexToLocation(index);
        }

        public Pixel(Point location, Color color, int imageWidth)
        {
            this.imageWidth = imageWidth;
            Color = color;
            Location = location;
            Index = LocationToIndex(location);
        }

        public object Clone()
        {
            return new Pixel(Location, Color, imageWidth);
        }

        public Point IndexToLocation(int index)
        {
            return new Point(index % imageWidth, index / imageWidth);
        }

        public int LocationToIndex(Point location)
        {
            return location.Y * imageWidth + location.X;
        }

        public static Point IndexToLocation(int imageWidth, int index)
        {
            return new Point(index % imageWidth, index / imageWidth);
        }

        public static int LocationToIndex(int imageWidth, Point location)
        {
            return location.Y * imageWidth + location.X;
        }
    }
}