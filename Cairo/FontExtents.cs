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
