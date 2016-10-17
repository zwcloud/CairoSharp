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
using System.Runtime.InteropServices;

namespace Cairo
{
	[StructLayout (LayoutKind.Sequential)]
	public struct TextExtents
	{
		double xbearing;
		double ybearing;
		double width;
		double height;
		double xadvance;
		double yadvance;
		
		public double XBearing {
			get { return xbearing; }
			set { xbearing = value; }
		}
		
		public double YBearing {
			get { return ybearing; }
			set { ybearing = value; }
		}
		
		public double Width {
			get { return width; }
			set { width = value; }
		}
		
		public double Height {
			get { return height; }
			set { height = value; }
		}
		
		public double XAdvance {
			get { return xadvance; }
			set { xadvance = value; }
		}
		
		public double YAdvance {
			get { return yadvance; }
			set { yadvance = value; }
		}

		public override bool Equals (object obj)
		{
			if (obj is TextExtents)
				return this == (TextExtents)obj;
			return false;
		}

		public override int GetHashCode ()
		{
			return (int)XBearing ^ (int)YBearing ^ (int)Width ^ (int)Height ^ (int)XAdvance ^ (int)YAdvance;
		}

		public static bool operator == (TextExtents extents, TextExtents other)
		{
			return extents.XBearing == other.XBearing && extents.YBearing == other.YBearing && extents.Width == other.Width && extents.Height == other.Height && extents.XAdvance == other.XAdvance && extents.YAdvance == other.YAdvance;
		}

		public static bool operator != (TextExtents extents, TextExtents other)
		{
			return !(extents == other);
		}
	}
}
