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
