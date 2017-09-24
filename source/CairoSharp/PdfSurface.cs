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

	public class PdfSurface : Surface
	{
		internal PdfSurface (IntPtr handle, bool owns) : base (handle, owns)
		{
		}

		public PdfSurface (string filename, double width, double height)
			: base (NativeMethods.cairo_pdf_surface_create (filename, width, height), true)
		{
		}

		public void SetSize (double width, double height)
		{
			CheckDisposed ();
			NativeMethods.cairo_pdf_surface_set_size (Handle, width, height);
		}
	}
}

