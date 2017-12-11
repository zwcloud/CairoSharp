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

	public class SurfacePattern : Pattern
	{
		internal SurfacePattern (IntPtr handle, bool owned) : base (handle, owned)
		{
		}

		public SurfacePattern (Surface surface)
			: base (NativeMethods.cairo_pattern_create_for_surface (surface.Handle), true)
		{
		}

		public Filter Filter {
			set {
				CheckDisposed ();
				NativeMethods.cairo_pattern_set_filter (Handle, value);
			}
			get {
				CheckDisposed ();
				return NativeMethods.cairo_pattern_get_filter (Handle);
			}
		}
	}
}

