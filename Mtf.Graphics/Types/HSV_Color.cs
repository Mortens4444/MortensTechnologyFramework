using System.Drawing;
using Mtf.Graphics.ColorExtensions;

namespace Mtf.Graphics.Types
{
	public class HSV_Color
	{
		public Color color;
		public int A;
		public int R;
		public int G;
		public int B;

		public float H;
		public float S;
		public float V;

		public HSV_Color(Color color)
		{
			A = color.A;
			R = color.R;
			G = color.G;
			B = color.B;
			this.color = color;

			H = color.GetHue();
			S = color.GetSaturation();
			V = color.GetBrightness();
		}

		public HSV_Color(float H, float S, float V)
		{
			this.H = H;
			this.S = S;
			this.V = V;

			color = BaseExtensions.ColorFromHSV(H, S, V);
			A = color.A;
			R = color.R;
			G = color.G;
			B = color.B;
		}

		public override string ToString()
		{
			return $"RGB = ({R}, {G}, {B})";
			//return $"HSV = ({H}, {S}, {V})";
		}
	}
}
