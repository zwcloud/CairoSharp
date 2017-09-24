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

namespace Cairo
{
	[Serializable]
	public enum Operator
	{
		Clear,

		Source,
		Over,
		In,
		Out,
		Atop,

		Dest,
		DestOver,
		DestIn,
		DestOut,
		DestAtop,
		
		Xor,
		Add,
        Saturate,

        Multiply,
        Screen,
        Overlay,
        Darken,
        Lighten,
        Color_dodge,
        Color_burn,
        Hard_light,
        Soft_light,
        Difference,
        Exclusion,
        HslHue,
        HslSaturation,
        HslColor,
        HslLuminosity
	}
}
