using System.Drawing;
using Mtf.Graphics.ColorExtensions;

namespace Mtf.Graphics.Types
{
	public class CMY_Color
	{
		public Color color;
		public int A;
		public int R;
		public int G;
		public int B;

		public int C;
		public int M;
		public int Y;

		public CMY_Color(Color color)
		{
			A = color.A;
			R = color.R;
			G = color.G;
			B = color.B;
			this.color = color;

			C = BaseExtensions.GetComponentValue(1 - GrayCalculatorExtensions.GetNonLinearGammaCorrectedValue(R));
			M = BaseExtensions.GetComponentValue(1 - GrayCalculatorExtensions.GetNonLinearGammaCorrectedValue(G));
			Y = BaseExtensions.GetComponentValue(1 - GrayCalculatorExtensions.GetNonLinearGammaCorrectedValue(B));
		}

		public CMY_Color(int C, int M, int Y)
		{
			this.C = C;
			this.M = M;
			this.Y = Y;

			A = 255;
			R = BaseExtensions.GetComponentValue(1 - GrayCalculatorExtensions.GetNonLinearGammaCorrectedValue(C));
			G = BaseExtensions.GetComponentValue(1 - GrayCalculatorExtensions.GetNonLinearGammaCorrectedValue(M));
			B = BaseExtensions.GetComponentValue(1 - GrayCalculatorExtensions.GetNonLinearGammaCorrectedValue(Y));
			color = Color.FromArgb(A, R, G, B);
		}

		public override string ToString()
		{
			return $"RGB = ({R}, {G}, {B})";
			//return $"CMY = ({C}, {M}, {Y})";
		}
	}
}
