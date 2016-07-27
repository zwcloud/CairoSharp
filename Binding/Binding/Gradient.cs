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

	public class Gradient : Pattern
	{
		protected Gradient (IntPtr handle, bool owned) : base (handle, owned)
		{
		}

		[Obsolete]
		protected Gradient ()
		{
		}

		public int ColorStopCount {
			get {
				CheckDisposed ();
				int cnt;
				NativeMethods.cairo_pattern_get_color_stop_count (Handle, out cnt);
				return cnt;
			}
		}

		public Status AddColorStop (double offset, Color c)
		{
			CheckDisposed ();
			NativeMethods.cairo_pattern_add_color_stop_rgba (Handle, offset, c.R, c.G, c.B, c.A);
			return Status;
		}

		public Status AddColorStopRgb (double offset, Color c)
		{
			CheckDisposed ();
			NativeMethods.cairo_pattern_add_color_stop_rgb (Handle, offset, c.R, c.G, c.B);
			return Status;
		}

		public Status AddColorStopRgba(double offset, double r, double g, double b, double a)
		{
			CheckDisposed();
			NativeMethods.cairo_pattern_add_color_stop_rgba(Handle, offset, r, g, b, a);
			return Status;
		}
	}
}

