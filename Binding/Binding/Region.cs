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

namespace Cairo
{

	[StructLayout(LayoutKind.Sequential)]
	public struct RectangleInt {
		public int X;
		public int Y;
		public int Width;
		public int Height;
	}

	public enum RegionOverlap {
		In,
		Out,
		Part,
	}

	public class Region : IDisposable {

		IntPtr handle;
		public IntPtr Handle {
			get { return handle; }
		}

		[Obsolete]
		public Region (IntPtr handle) : this (handle, false) {}

		public Region (IntPtr handle, bool owned)
		{
			if (handle == IntPtr.Zero)
				throw new ArgumentException ("handle should not be NULL", "handle");

			this.handle = handle;
			if (!owned)
				NativeMethods.cairo_region_reference (handle);
			if (CairoDebug.Enabled)
				CairoDebug.OnAllocated (handle);
		}

		public Region () : this (NativeMethods.cairo_region_create () , true)
		{
		}

		public Region (RectangleInt rect)
		{
			handle = NativeMethods.cairo_region_create_rectangle (ref rect);
		}

		public Region (RectangleInt[] rects)
		{
			handle = NativeMethods.cairo_region_create_rectangles (rects, rects.Length);
		}

		public Region Copy ()
		{
			CheckDisposed ();
			return new Region (NativeMethods.cairo_region_copy (Handle), true);
		}

		~Region ()
		{
			Dispose (false);
		}

		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		protected virtual void Dispose (bool disposing)
		{
			if (!disposing || CairoDebug.Enabled)
				CairoDebug.OnDisposed<Region> (handle, disposing);

			if (handle == IntPtr.Zero)
				return;

			NativeMethods.cairo_region_destroy (Handle);
			handle = IntPtr.Zero;
		}

		void CheckDisposed ()
		{
			if (handle == IntPtr.Zero)
				throw new ObjectDisposedException ("Object has already been disposed");
		}

		public override bool Equals (object obj)
		{
			return (obj is Region) && NativeMethods.cairo_region_equal (Handle, (obj as Region).Handle);
		}

		public override int GetHashCode ()
		{
			return Handle.GetHashCode ();
		}

		public Status Status {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_region_status (Handle); }
		}

		public RectangleInt Extents {
			get {
				CheckDisposed ();
				RectangleInt result;
				NativeMethods.cairo_region_get_extents (Handle, out result);
				return result;
			}
		}

		public int NumRectangles {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_region_num_rectangles (Handle);
			}
		}

		public RectangleInt GetRectangle (int nth)
		{
			CheckDisposed ();
			RectangleInt val;
			NativeMethods.cairo_region_get_rectangle (Handle, nth, out val);
			return val;
		}

		public bool IsEmpty {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_region_is_empty (Handle);
			}
		}

		public RegionOverlap ContainsPoint (RectangleInt rectangle)
		{
			CheckDisposed ();
			return NativeMethods.cairo_region_contains_rectangle (Handle, ref rectangle);
		}

		public bool ContainsPoint (int x, int y)
		{
			CheckDisposed ();
			return NativeMethods.cairo_region_contains_point (Handle, x, y);
		}

		public void Translate (int dx, int dy)
		{
			CheckDisposed ();
			NativeMethods.cairo_region_translate (Handle, dx, dy);
		}

		public Status Subtract (Region other)
		{
			CheckDisposed ();
			return NativeMethods.cairo_region_subtract (Handle, other.Handle);
		}

		public Status SubtractRectangle (RectangleInt rectangle)
		{
			CheckDisposed ();
			return NativeMethods.cairo_region_subtract_rectangle (Handle, ref rectangle);
		}

		public Status Intersect (Region other)
		{
			CheckDisposed ();
			return NativeMethods.cairo_region_intersect (Handle, other.Handle);
		}

		public Status IntersectRectangle (RectangleInt rectangle)
		{
			CheckDisposed ();
			return NativeMethods.cairo_region_intersect_rectangle (Handle, ref rectangle);
		}

		public Status Union (Region other)
		{
			CheckDisposed ();
			return NativeMethods.cairo_region_union (Handle, other.Handle);
		}

		public Status UnionRectangle (RectangleInt rectangle)
		{
			CheckDisposed ();
			return NativeMethods.cairo_region_union_rectangle (Handle, ref rectangle);
		}

		public Status Xor (Region other)
		{
			CheckDisposed ();
			return NativeMethods.cairo_region_xor (Handle, other.Handle);
		}

		public Status XorRectangle (RectangleInt rectangle)
		{
			CheckDisposed ();
			return NativeMethods.cairo_region_xor_rectangle (Handle, ref rectangle);
		}
	}
}
