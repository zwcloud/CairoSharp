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
using System.IO;
using System.Runtime.InteropServices;

namespace Cairo {

	public class SvgSurface : Surface
	{
		internal SvgSurface (IntPtr handle, bool owns) : base (handle, owns)
		{
		}

		public SvgSurface (string filename, double width, double height)
			: base (NativeMethods.cairo_svg_surface_create (filename, width, height), true)
		{
		}

	    public SvgSurface(Stream stream, double width, double height) :
	        base (NativeMethods.cairo_svg_surface_create_for_stream (CreateWriteFunc(stream), IntPtr.Zero, width, height), true)
	    {
	    }

        public void RestrictToVersion (SvgVersion version)
		{
			CheckDisposed ();
			NativeMethods.cairo_svg_surface_restrict_to_version (Handle, version);
		}

	    private static NativeMethods.cairo_write_func_t CreateWriteFunc(Stream stream)
	    {
            if(stream == null || !stream.CanWrite)
                throw new ArgumentException();

	        return (closure, in_data, length) =>
	        {
	            byte[] tempBuff = new byte[length];
	            Marshal.Copy(in_data, tempBuff, 0, length);

	            stream.Write(tempBuff, 0, tempBuff.Length);

	            return Status.Success;
	        };
	    }
    }
}

