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

