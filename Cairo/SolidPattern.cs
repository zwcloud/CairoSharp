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

namespace Cairo {

	public class SolidPattern : Pattern
	{
		internal SolidPattern (IntPtr handle, bool owned) : base (handle, owned)
		{
		}

		public SolidPattern (Color color)
			: base (NativeMethods.cairo_pattern_create_rgba (color.R, color.G, color.B, color.A), true)
		{
		}

		public SolidPattern (double r, double g, double b)
			: base (NativeMethods.cairo_pattern_create_rgb (r, g, b), true)
		{
		}

		public SolidPattern (double r, double g, double b, double a)
			: base (NativeMethods.cairo_pattern_create_rgba (r, g, b, a), true)
		{
		}

		public SolidPattern (Color color, bool solid)
			: base (solid
					? NativeMethods.cairo_pattern_create_rgb (color.R, color.G, color.B)
					: NativeMethods.cairo_pattern_create_rgba (color.R, color.G, color.B, color.A),
				true)
		{
		}

		public Color Color {
			get {
				CheckDisposed ();
				double red, green, blue, alpha;
				NativeMethods.cairo_pattern_get_rgba  (Handle, out red, out green, out blue, out alpha);
				return new Color (red, green, blue, alpha);
			}
		}
	}
}

