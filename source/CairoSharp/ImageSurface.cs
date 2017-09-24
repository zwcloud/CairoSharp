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

        public ImageSurface(byte[] pngData)
            : base(ConstructImageSurfaceFromPngData(pngData), true)
        {
            offset = 0;
        }

	    private static int offset;

        private static IntPtr ConstructImageSurfaceFromPngData(byte[] pngData)
        {
            NativeMethods.cairo_read_func_t func = delegate(IntPtr closure, IntPtr out_data, int length)
            {
                Marshal.Copy(pngData, offset, out_data, length);
                offset += length;
                return Status.Success;
            };
            return NativeMethods.cairo_image_surface_create_from_png_stream(func, IntPtr.Zero);
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
