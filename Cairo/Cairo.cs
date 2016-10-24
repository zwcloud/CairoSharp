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

namespace Cairo
{
	public static class CairoAPI {
		static public int Version {
			get {
				return Cairo.NativeMethods.cairo_version ();
			}
		}

		static public string VersionString {
			get {
				IntPtr x = Cairo.NativeMethods.cairo_version_string ();
				return Marshal.PtrToStringAnsi (x);
			}
		}
	}
}
