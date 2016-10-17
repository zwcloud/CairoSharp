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
	[StructLayout(LayoutKind.Sequential)]
	public struct Glyph
	{
		internal long index;
		internal double x;
		internal double y;
		
		public Glyph (long index, double x, double y)
		{
			this.index = index;
			this.x = x;
			this.y = y;
		}
		
		public long Index {
			get { return index; }
			set { index = value; }
		}
		
		public double X {
			get { return x; }
			set { x = value; }
		}
		
		public double Y {
			get { return y; }
			set { y = value; }
		}

		public override bool Equals (object obj)
		{
			if (obj is Glyph)
				return this == (Glyph)obj;
			return false;
		}

		public override int GetHashCode ()
		{
			return (int) Index ^ (int) X ^ (int) Y;
		}

		internal static IntPtr GlyphsToIntPtr (Glyph[] glyphs)
		{
			int size = Marshal.SizeOf (glyphs[0]);
			IntPtr dest = Marshal.AllocHGlobal (size * glyphs.Length);
			long pos = dest.ToInt64 ();
			for (int i = 0; i < glyphs.Length; i++, pos += size)
				Marshal.StructureToPtr (glyphs[i], (IntPtr) pos, false);
			return dest;
		}

		public static bool operator == (Glyph glyph, Glyph other)
		{
			return glyph.Index == other.Index && glyph.X == other.X && glyph.Y == other.Y;
		}

		public static bool operator != (Glyph glyph, Glyph other)
		{
			return !(glyph == other);
		}
	}
}
