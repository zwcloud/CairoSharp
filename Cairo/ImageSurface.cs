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
using System.Runtime.InteropServices;

namespace Cairo {

	public class ImageSurface : Surface
	{
		internal ImageSurface (IntPtr handle, bool owns) : base (handle, owns)
		{
		}

		public ImageSurface (Format format, int width, int height)
			: base (NativeMethods.cairo_image_surface_create (format, width, height), true)
		{
		}

		[Obsolete ("Use ImageSurface (byte[] data, Cairo.Format format, int width, int height, int stride)")]
		public ImageSurface (ref byte[] data, Cairo.Format format, int width, int height, int stride)
			: this (data, format, width, height, stride)
		{
		}

		public ImageSurface (byte[] data, Format format, int width, int height, int stride)
			: base (NativeMethods.cairo_image_surface_create_for_data (data, format, width, height, stride), true)
		{
		}

		public ImageSurface (IntPtr data, Format format, int width, int height, int stride)
			: base (NativeMethods.cairo_image_surface_create_for_data (data, format, width, height, stride), true)
		{
		}

		public ImageSurface (string filename)
			: base (NativeMethods.cairo_image_surface_create_from_png (filename), true)
		{
		}

		public int Width {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_image_surface_get_width (Handle); }
		}

		public int Height {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_image_surface_get_height (Handle);
			}
		}

		public byte[] Data {
			get {
				IntPtr ptr = NativeMethods.cairo_image_surface_get_data (Handle);
				int length = Height * Stride;
				byte[] data = new byte[length];
				Marshal.Copy (ptr, data, 0, length);
				return data;
			}
		}

		public IntPtr DataPtr {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_image_surface_get_data (Handle);
			}
		}

		public Format Format {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_image_surface_get_format (Handle);
			}
		}

		public int Stride {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_image_surface_get_stride (Handle);
			}
		}
	}
}
