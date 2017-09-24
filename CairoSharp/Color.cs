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

	public struct Color
	{
		public Color(double r, double g, double b) : this (r, g, b, 1.0)
		{
		}

		public Color(double r, double g, double b, double a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		double r, g, b, a;

		public double R {
			get { return r; }
			set { r = value; }
		}

		public double G {
			get { return g; }
			set { g = value; }
		}

		public double B {
			get { return b; }
			set { b = value; }
		}

		public double A {
			get { return a; }
			set { a = value; }
		}

        public static bool operator==(Color a, Color b)
        {
            return a.Equals(b);
        }

	    public static bool operator!=(Color a, Color b)
	    {
	        return !(a == b);
	    }

	    #region Overrides of ValueType

	    public override bool Equals(object obj)
	    {
	        Color other = (Color) obj;
	        return System.Math.Abs(this.r - other.r) < double.Epsilon
	               && System.Math.Abs(this.g - other.g) < double.Epsilon
	               && System.Math.Abs(this.b - other.b) < double.Epsilon
	               && System.Math.Abs(this.a - other.a) < double.Epsilon;
	    }

	    public override int GetHashCode()
	    {
	        return base.GetHashCode();
	    }

	    #endregion
	}
}
