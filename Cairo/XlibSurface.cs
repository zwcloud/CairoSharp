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

	public class XlibSurface : Surface
	{
		public XlibSurface (IntPtr display, IntPtr drawable, IntPtr visual, int width, int height)
			: base (NativeMethods.cairo_xlib_surface_create (display, drawable, visual, width, height), true)
		{
		}

		public XlibSurface (IntPtr ptr, bool own) : base (ptr, own)
		{
		}

		public static XlibSurface FromBitmap (IntPtr display, IntPtr bitmap, IntPtr screen, int width, int height)
		{
			IntPtr ptr = NativeMethods.cairo_xlib_surface_create_for_bitmap (display, bitmap, screen, width, height);
			return new XlibSurface(ptr, true);
		}

		public void SetDrawable (IntPtr drawable, int width, int height)
		{
			CheckDisposed ();
			NativeMethods.cairo_xlib_surface_set_drawable (Handle, drawable, width, height);
		}

		public void SetSize (int width, int height)
		{
			CheckDisposed ();
			NativeMethods.cairo_xlib_surface_set_size (Handle, width, height);
		}

		public int Depth {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_xlib_surface_get_depth (Handle);
			}
		}
		
		public IntPtr Display {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_xlib_surface_get_display (Handle);
			}
		}

		public IntPtr Drawable {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_xlib_surface_get_drawable (Handle);
			}
		}

		public int Height {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_xlib_surface_get_height (Handle);
			}
		}

		public IntPtr Screen {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_xlib_surface_get_screen (Handle);
			}
		}

		public IntPtr Visual {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_xlib_surface_get_visual (Handle);
			}
		}

		public int Width {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_xlib_surface_get_width (Handle);
			}
		}

	}
}
