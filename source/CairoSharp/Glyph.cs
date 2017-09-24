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
