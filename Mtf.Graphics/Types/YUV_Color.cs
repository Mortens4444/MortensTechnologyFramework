using System;
using System.Drawing;
using Mtf.Graphics.Enum;

namespace Mtf.Graphics.Types
{
	public class YUV_Color
	{
		public byte Y;
		public byte U;
		public byte V;

		private int C;
		private int D;
		private int E;

		public byte R;
		public byte G;
		public byte B;

		public YUV_Color(Color color)
			: this(color.R, color.G, color.B, ColorSpaceType.RGB)
		{
		}

		public YUV_Color(int r_c_y, int g_d_u, int b_e_v, ColorSpaceType type)
		{
			switch (type)
			{
				case ColorSpaceType.RGB:
					C = Convert.ToInt32((66 * r_c_y + 129 * g_d_u + 25 * b_e_v + 128) >> 8);
					D = Convert.ToInt32((-38 * r_c_y - 74 * g_d_u + 112 * b_e_v + 128) >> 8);
					E = Convert.ToInt32((112 * r_c_y - 94 * g_d_u - 18 * b_e_v + 128) >> 8);
					Y = Convert.ToByte(C + 16);
					U = Convert.ToByte(D + 128);
					V = Convert.ToByte(E + 128);
					R = Convert.ToByte(r_c_y);
					G = Convert.ToByte(g_d_u);
					B = Convert.ToByte(b_e_v);
					break;
				case ColorSpaceType.CDE:
					C = r_c_y;
					D = g_d_u;
					E = b_e_v;
					Y = Convert.ToByte(C + 16);
					U = Convert.ToByte(D + 128);
					V = Convert.ToByte(E + 128);
					R = Clip((298 * C + 409 * E + 128) >> 8);
					G = Clip((298 * C - 100 * D - 208 * E + 128) >> 8);
					B = Clip((298 * C + 516 * D + 128) >> 8);
					break;
				case ColorSpaceType.YUV:
					Y = Convert.ToByte(r_c_y);
					U = Convert.ToByte(g_d_u);
					V = Convert.ToByte(b_e_v);
					C = Convert.ToInt32(Y - 16);
					D = Convert.ToInt32(U - 128);
					E = Convert.ToInt32(V - 128);
					R = Clip((298 * C + 409 * E + 128) >> 8);
					G = Clip((298 * C - 100 * D - 208 * E + 128) >> 8);
					B = Clip((298 * C + 516 * D + 128) >> 8);
					break;
			    default:
			        throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}
		}

		private static byte Clip(int value)
		{
			return Convert.ToByte(Math.Max(Math.Min(value, 255), 0));
		}

		public Color ToColor()
		{
			return ConvertToColor(this);
		}

		public static Color ConvertToColor(YUV_Color yuvColor)
		{
			return Color.FromArgb(yuvColor.R, yuvColor.G, yuvColor.B);
		}

		public override string ToString()
		{
			return $"RGB = ({R}, {G}, {B})";
			//return $"YUV = ({Y}, {U}, {V})";
		}
	}
}
