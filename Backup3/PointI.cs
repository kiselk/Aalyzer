using System;
using System.Drawing;

namespace GraphicsControlTest
{
	/// <summary>
	/// Summary description for PointI.
	/// </summary>
	public class PointI
	{

		public int X = 0;
		public int Y = 0;

	  
		public PointI()
		{
		}

		public PointI(Point p)
		{

			X = p.X;
			Y = p.Y;
		}

		public PointI(int x, int y)
		{
			X = x;
			Y = y;
		}

		public Point ToPoint()
		{
			return new Point(X, Y);
		}



	}
}
