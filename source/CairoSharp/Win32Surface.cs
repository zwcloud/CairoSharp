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
	
    /// <summary>
    /// Creates a new Win32Surface from a valid hdc
    /// </summary>
    /// <remarks>the format of this surface is Format.Rgb24</remarks>
	public class Win32Surface : Surface
	{
		internal Win32Surface (IntPtr handle, bool owns) : base (handle, owns)
		{
		}
		
		public Win32Surface (IntPtr hdc)
			: base (NativeMethods.cairo_win32_surface_create (hdc), true)
		{
		}
	}
}
