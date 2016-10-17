// Copyright (C) 2015  Zou Wei, zwcloud@yeah.net, http://zwcloud.net
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 3 of the License, or (at your option) any later version.
// 
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Suite 500, Boston, MA 02110-1335, USA
//
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
