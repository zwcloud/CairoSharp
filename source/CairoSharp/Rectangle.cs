#region copyright
// Copyright (C) 2015-2016  Zou Wei, zwcloud@hotmail.com, http://zwcloud.net
// Copyright (C) 2007-2015  Xamarin, Inc.
// Copyright (C) 2006 Alp Toker
// Copyright (C) 2005 John Luke
// Copyright (C) 2004 Novell, Inc (http://www.novell.com)
// Copyright (C) Ximian, Inc. 2003
// Licensed under the GNU LGPL v3, see LICENSE for more infomation.
#endregion
using System;

namespace Cairo
{
	public struct Rectangle
	{
		double x;
		double y;
		double width;
		double height;
		
		public Rectangle (double x, double y, double width, double height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}
		
		public Rectangle (Point point, double width, double height)
		{
			x = point.X;
			y = point.Y;
			this.width = width;
			this.height = height;
		}
		
		public double X {
			get { return x; }
		}
		
		public double Y {
			get { return y; }
		}
		
		public double Width {
			get { return width; }
		}
		
		public double Height {
			get { return height; }
		}
		
		public override bool Equals (object obj)
		{
			if (obj is Rectangle)
				return this == (Rectangle)obj;
			return false;
		}
		
		public override int GetHashCode ()
		{
			return (int) (x + y + width + height);
		}

		public override string ToString ()
		{
			return String.Format ("x:{0} y:{1} w:{2} h:{3}", x, y, width, height);
		}
		
		public static bool operator == (Rectangle rectangle, Rectangle other)
		{
			return rectangle.X == other.X && rectangle.Y == other.Y && rectangle.Width == other.Width && rectangle.Height == other.Height;
		}
		
		public static bool operator != (Rectangle rectangle, Rectangle other)
		{
			return !(rectangle == other);
		}
	}
}
