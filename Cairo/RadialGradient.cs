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

	public class RadialGradient : Gradient
	{
		internal RadialGradient (IntPtr handle, bool owned) : base (handle, owned)
		{
		}

		public RadialGradient (double cx0, double cy0, double radius0, double cx1, double cy1, double radius1)
			: base (NativeMethods.cairo_pattern_create_radial (cx0, cy0, radius0, cx1, cy1, radius1), true)
		{
		}
	}
}

