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
