using System;
using System.Drawing;

namespace Mtf.Graphics.Types
{
	public class CMYK_Color
	{
		public Color color;
		public int A;
		public int R;
		public int G;
		public int B;

		public int C;
		public int M;
		public int Y;
		public int K;

		public CMYK_Color(Color color)
		{
			A = color.A;
			R = color.R;
			G = color.G;
			B = color.B;
			this.color = color;

			var cmy = new CMY_Color(color);
			K = Math.Min(Math.Min(cmy.C, cmy.M), cmy.Y);
			C = cmy.C - K;
			M = cmy.M - K;
			Y = cmy.Y - K;
		}

		public CMYK_Color(int C, int M, int Y, int K)
		{
			this.C = C;
			this.M = M;
			this.Y = Y;
			this.K = K;

			var cmy = new CMY_Color(this.C + this.K, this.M + this.K, this.Y + this.K);

			A = 255;
			R = cmy.color.R;
			G = cmy.color.G;
			B = cmy.color.B;
			color = cmy.color;
		}

		public override string ToString()
		{
			return $"RGB = ({R}, {G}, {B})";
			//return $"CMYK = ({C}, {M}, {Y}, {K})";
		}
	}
}
