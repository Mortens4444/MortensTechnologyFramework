using System;
using System.Drawing;
using Mtf.Graphics.ColorExtensions;

namespace Mtf.Graphics.Types
{
	public class YCbCr_Color
	{
		public Color color;
		public int A;
		public int R;
		public int G;
		public int B;

		public double LinearGammaCorrected_Red;
		public double LinearGammaCorrected_Green;
		public double LinearGammaCorrected_Blue;

		public double Y_Apostrophe;
		public double U;
		public double V;

		public double Pb;
		public double Pr;

		public int Y;
		public int Cb;
		public int Cr;

		private const double x = 0.565; /* (0.5 / (1 - 0.114)) = 0.56433408577878103837471783295711 */
		private const double y = 0.713; /* (0.5 / (1 - 0.299)) = 0.71326676176890156918687589158345 */
		
		public YCbCr_Color(Color color)
		{
			A = color.A;
			R = color.R;
			G = color.G;
			B = color.B;
			this.color = color;

			LinearGammaCorrected_Red = GrayCalculatorExtensions.GetNonLinearGammaCorrectedValue(color.R);
			LinearGammaCorrected_Green = GrayCalculatorExtensions.GetNonLinearGammaCorrectedValue(color.G);
			LinearGammaCorrected_Blue = GrayCalculatorExtensions.GetNonLinearGammaCorrectedValue(color.B);

			Y_Apostrophe = color.GetBT601Value();
			U = LinearGammaCorrected_Blue - Y_Apostrophe;
			V = LinearGammaCorrected_Red - Y_Apostrophe;

			Pb = x * U;
			Pr = y * V;

			Y = (int)Math.Round(16 + 219 * Y_Apostrophe);
			Cb = (int)Math.Round(128 + 224 * Pb);
			Cr = (int)Math.Round(128 + 224 * Pr);
		}

		public YCbCr_Color(int Y, int Cb, int Cr)
		{
			this.Y = Y;
			this.Cb = Cb;
			this.Cr = Cr;

			Pb = (this.Cb - 128) / 224.0;
			Pr = (this.Cr - 128) / 224.0;

			U = Pb / x;
			V = Pr / y;

			Y_Apostrophe = (this.Y - 16) / 219.0;
			//LinearGammaCorrected_Red = V + this.Y_Apostrophe;
			//LinearGammaCorrected_Blue = U + this.Y_Apostrophe;
			//LinearGammaCorrected_Green = (Y_Apostrophe - 0.299 * LinearGammaCorrected_Red - 0.114 * LinearGammaCorrected_Blue) / 0.587;

			LinearGammaCorrected_Red = Y_Apostrophe + 1.403 * Pr;
			LinearGammaCorrected_Blue = Y_Apostrophe + 1.770 * Pb;
			LinearGammaCorrected_Green = Y_Apostrophe - 0.344 * Pb - 0.714 * Pr;

			A = 255;
			R = Math.Min(Math.Max(BaseExtensions.GetComponentValue(LinearGammaCorrected_Red), 0), 255);
			G = Math.Min(Math.Max(BaseExtensions.GetComponentValue(LinearGammaCorrected_Green), 0), 255);
			B = Math.Min(Math.Max(BaseExtensions.GetComponentValue(LinearGammaCorrected_Blue), 0), 255);
			color = Color.FromArgb(A, R, G, B);
		}

		public override string ToString()
		{
			return $"RGB = ({R}, {G}, {B})";
			//return $"YCbCr = ({Y}, {Cb}, {Cr})";
		}
	}
}
