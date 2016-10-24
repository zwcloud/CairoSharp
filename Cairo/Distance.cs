#region copyright
// Copyright (C) 2015-2016  Zou Wei, zwcloud@hotmail.com, http://zwcloud.net
// Copyright (C) 2007-2015  Xamarin, Inc.
// Copyright (C) 2006 Alp Toker
// Copyright (C) 2005 John Luke
// Copyright (C) 2004 Novell, Inc (http://www.novell.com)
// Copyright (C) Ximian, Inc. 2003
// Licensed under the GNU LGPL v3, see LICENSE for more infomation.
#endregion
namespace Cairo {

	public struct Distance
	{
		public Distance (double dx, double dy)
		{
			this.dx = dx;
			this.dy = dy;
		}

		double dx, dy;
		public double Dx {
			get { return dx; }
			set { dx = value; }
		}

		public double Dy {
			get { return dy; }
			set { dy = value; }
		}
	}
}
