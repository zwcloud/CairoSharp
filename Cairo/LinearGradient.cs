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

	public class LinearGradient : Gradient
	{
		internal LinearGradient (IntPtr handle, bool owned) : base (handle, owned)
		{
		}

		public LinearGradient (double x0, double y0, double x1, double y1)
			: base (NativeMethods.cairo_pattern_create_linear (x0, y0, x1, y1), true)
		{
		}

		public PointD[] LinearPoints {
			get {
				CheckDisposed ();
				double x0, y0, x1, y1;
				PointD[] points = new PointD [2];

				NativeMethods.cairo_pattern_get_linear_points (Handle, out x0, out y0, out x1, out y1);

				points[0] = new PointD (x0, y0);
				points[1] = new PointD (x1, y1);
				return points;
			}
		}
	}
}

