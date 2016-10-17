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
