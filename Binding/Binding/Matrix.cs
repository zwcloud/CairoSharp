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
using System.Runtime.InteropServices;

namespace Cairo {

	[StructLayout(LayoutKind.Sequential)]
	public class Matrix : ICloneable
	{
		public double Xx;
		public double Yx;
		public double Xy;
		public double Yy;
		public double X0;
		public double Y0;

		public Matrix (double xx, double yx, double xy, double yy,
				double x0, double y0)
		{
			this.Xx = xx; this.Yx = yx; this.Xy = xy;
			this.Yy = yy; this.X0 = x0; this.Y0 = y0;
		}

		public Matrix ()
		{
			this.InitIdentity ();
		}

		public bool IsIdentity ()
		{
			return (this == new Matrix ());
		}

		public void InitIdentity ()
		{
			// this.Init(1,0,0,1,0,0);
			NativeMethods.cairo_matrix_init_identity (this);
		}

		public void Init (double xx, double yx, double xy, double yy,
				  double x0, double y0)
		{
			this.Xx = xx; this.Yx = yx; this.Xy = xy;
			this.Yy = yy; this.X0 = x0; this.Y0 = y0;
		}

		public void InitTranslate (double tx, double ty)
		{
			//this.Init (1, 0, 0, 1, tx, ty);
			NativeMethods.cairo_matrix_init_translate (this, tx, ty);
		}

		public void Translate (double tx, double ty)
		{
			NativeMethods.cairo_matrix_translate (this, tx, ty);
		}

		public void InitScale (double sx, double sy)
		{
			//this.Init (sx, 0, 0, sy, 0, 0);
			NativeMethods.cairo_matrix_init_scale (this, sx, sy);
		}

		public void Scale (double sx, double sy)
		{
			NativeMethods.cairo_matrix_scale (this, sx, sy);
		}

		public void InitRotate (double radians)
		{
			/*
			double s, c;
			s = Math.Sin (radians);
			c = Math.Cos (radians);
			this.Init (c, s, -s, c, 0, 0);
			*/
			NativeMethods.cairo_matrix_init_rotate (this, radians);
		}

		public void Rotate (double radians)
		{
			NativeMethods.cairo_matrix_rotate (this, radians);
		}

		public Cairo.Status Invert ()
		{
			return NativeMethods.cairo_matrix_invert (this);
		}

		public void Multiply (Matrix b)
		{
			Matrix a = (Matrix) this.Clone ();
			NativeMethods.cairo_matrix_multiply (this, a, b);
		}

		public static Matrix Multiply (Matrix a, Matrix b) {
			Matrix result = new Matrix ();
			NativeMethods.cairo_matrix_multiply (result, a, b);
			return result;
		}


		public void TransformDistance (ref double dx, ref double dy)
		{
			NativeMethods.cairo_matrix_transform_distance (this, ref dx, ref dy);
		}

		public void TransformPoint (ref double x, ref double y)
		{
			NativeMethods.cairo_matrix_transform_point (this, ref x, ref y);
		}

		public override String ToString ()
		{
			String s = String.Format ("xx:{0:##0.0#} yx:{1:##0.0#} xy:{2:##0.0#} yy:{3:##0.0#} x0:{4:##0.0#} y0:{5:##0.0#}",
				this.Xx, this.Yx, this.Xy, this.Yy, this.X0, this.Y0);
			return s;
		}

		public static bool operator == (Matrix lhs, Matrix rhs)
		{
			return (lhs.Xx == rhs.Xx &&
				lhs.Xy == rhs.Xy &&
				lhs.Yx == rhs.Yx &&
				lhs.Yy == rhs.Yy &&
				lhs.X0 == rhs.X0 &&
				lhs.Y0 == rhs.Y0 );
		}

		public static bool operator != (Matrix lhs, Matrix rhs)
		{
			return !(lhs==rhs);
		}



		public override bool Equals(object o)
		{
			if (! (o is Matrix))
				return false;
			else
				return (this == (Matrix) o);
		}

		public override int GetHashCode()
		{
			return  (int)this.Xx ^ (int)this.Xx>>32 ^
				(int)this.Xy ^ (int)this.Xy>>32 ^
				(int)this.Yx ^ (int)this.Yx>>32 ^
				(int)this.Yy ^ (int)this.Yy>>32 ^
				(int)this.X0 ^ (int)this.X0>>32 ^
				(int)this.Y0 ^ (int)this.Y0>>32;
		}

		public object Clone()
		{
			return this.MemberwiseClone ();
		}

	}
}
