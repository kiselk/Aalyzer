using System;
using System.Drawing;

namespace GraphicsControlTest
{
	/// <summary>
	/// Summary description for PointI.
	/// </summary>
	public class PointFloat
	{

		public float X = 0;
		public float Y = 0;

	  
		public PointFloat()
		{
		}

		public PointFloat(PointF p)
		{

			X = p.X;
			Y = p.Y;
		}

		public PointFloat(float x, float y)
		{
			X = x;
			Y = y;
		}

		public PointF ToPoint()
		{
			return new PointF(X, Y);
		}



	}
}
