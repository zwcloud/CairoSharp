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

