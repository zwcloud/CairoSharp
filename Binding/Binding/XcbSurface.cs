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
	public class XcbSurface : Surface
	{
		internal XcbSurface (IntPtr handle, bool owns) : base (handle, owns)
		{
		}

		public XcbSurface (IntPtr connection, uint drawable, IntPtr visual, int width, int height)
			: base (NativeMethods.cairo_xcb_surface_create (connection, drawable, visual, width, height), true)
		{
		}

		public static XcbSurface FromBitmap (IntPtr connection, uint bitmap, IntPtr screen, int width, int height)
		{
			IntPtr ptr = NativeMethods.cairo_xcb_surface_create_for_bitmap (connection, bitmap, screen, width, height);
			return new XcbSurface (ptr, true);
		}

		public void SetSize (int width, int height)
		{
			CheckDisposed ();
			NativeMethods.cairo_xcb_surface_set_size (Handle, width, height);
		}
	}
}
