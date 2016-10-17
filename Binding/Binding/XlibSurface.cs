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
