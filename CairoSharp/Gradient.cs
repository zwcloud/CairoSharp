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

	public class Gradient : Pattern
	{
		protected Gradient (IntPtr handle, bool owned) : base (handle, owned)
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

