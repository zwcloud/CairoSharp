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

	public class SvgSurface : Surface
	{
		internal SvgSurface (IntPtr handle, bool owns) : base (handle, owns)
		{
		}

		public SvgSurface (string filename, double width, double height)
			: base (NativeMethods.cairo_svg_surface_create (filename, width, height), true)
		{
		}

		public void RestrictToVersion (SvgVersion version)
		{
			CheckDisposed ();
			NativeMethods.cairo_svg_surface_restrict_to_version (Handle, version);
		}
	}
}

