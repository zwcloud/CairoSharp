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
	public struct FontExtents
	{
		double ascent;
		double descent;
		double height;
		double maxXAdvance;
		double maxYAdvance;
		
		public double Ascent {
			get { return ascent; }
			set { ascent = value; }
		}
		
		public double Descent {
			get { return descent; }
			set { descent = value; }
		}
		
		public double Height {
			get { return height; }
			set { height = value; }
		}
		
		public double MaxXAdvance {
			get { return maxXAdvance; }
			set { maxXAdvance = value; }
		}
		
		public double MaxYAdvance {
			get { return maxYAdvance; }
			set { maxYAdvance = value; }
		}

		public FontExtents (double ascent, double descent, double height, double maxXAdvance, double maxYAdvance)
		{
			this.ascent = ascent;
			this.descent = descent;
			this.height = height;
			this.maxXAdvance = maxXAdvance;
			this.maxYAdvance = maxYAdvance;
		}

		public override bool Equals (object obj)
		{
			if (obj is FontExtents)
				return this == (FontExtents) obj;
			return false;
		}

		public override int GetHashCode ()
		{
			return (int) Ascent ^ (int) Descent ^ (int) Height ^ (int) MaxXAdvance ^ (int) MaxYAdvance;
		}

		public static bool operator == (FontExtents extents, FontExtents other)
		{
			return extents.Ascent == other.Ascent && extents.Descent == other.Descent && extents.Height == other.Height && extents.MaxXAdvance == other.MaxXAdvance && extents.MaxYAdvance == other.MaxYAdvance;
		}

		public static bool operator != (FontExtents extents, FontExtents other)
		{
			return !(extents == other);
		}
	}
}
