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
