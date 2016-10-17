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
using System.Collections;

namespace Cairo {
   
	public class Pattern : IDisposable
	{
		[Obsolete]
		protected IntPtr pattern = IntPtr.Zero;

		public static Pattern Lookup (IntPtr pattern, bool owner)
		{
			if (pattern == IntPtr.Zero)
				return null;
			
			PatternType pt = NativeMethods.cairo_pattern_get_type (pattern);
			switch (pt) {
			case PatternType.Solid:
				return new SolidPattern (pattern, owner);
			case PatternType.Surface:
				return new SurfacePattern (pattern, owner);
			case PatternType.Linear:
				return new LinearGradient (pattern, owner);
			case PatternType.Radial:
				return new RadialGradient (pattern, owner);
			default:
				return new Pattern (pattern, owner);
			}
		}

		[Obsolete]
		protected Pattern ()
		{
		}
		
		internal Pattern (IntPtr handle, bool owned)
		{
			if (handle == IntPtr.Zero)
				throw new ArgumentException ("handle should not be NULL", "handle");

			Handle = handle;
			if (!owned)
				NativeMethods.cairo_pattern_reference (handle);
			if (CairoDebug.Enabled)
				CairoDebug.OnAllocated (handle);
		}

		~Pattern ()
		{
			Dispose (false);
		}
		
		[Obsolete ("Use the SurfacePattern constructor")]
		public Pattern (Surface surface)
			: this ( NativeMethods.cairo_pattern_create_for_surface (surface.Handle), true)
		{
		}
		
		[Obsolete]
		protected void Reference ()
		{
			CheckDisposed ();
			NativeMethods.cairo_pattern_reference (pattern);
		}

		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		protected virtual void Dispose (bool disposing)
		{
			if (!disposing || CairoDebug.Enabled)
				CairoDebug.OnDisposed<Pattern> (Handle, disposing);

			if (Handle == IntPtr.Zero)
				return;

			NativeMethods.cairo_pattern_destroy (Handle);
			Handle = IntPtr.Zero;
		}

		protected void CheckDisposed ()
		{
			if (Handle == IntPtr.Zero)
				throw new ObjectDisposedException ("Object has already been disposed");
		}

		[Obsolete ("Use Dispose()")]
		public void Destroy ()
		{
			Dispose ();
		}

		public Status Status
		{
			get {
				CheckDisposed ();
				return NativeMethods.cairo_pattern_status (Handle);
			}
		}

		public Extend Extend
		{
			get {
				CheckDisposed ();
				return NativeMethods.cairo_pattern_get_extend (Handle);
			}
			set {
				CheckDisposed ();
				NativeMethods.cairo_pattern_set_extend (Handle, value);
			}
		}

		public Matrix Matrix {
			set {
				CheckDisposed ();
				NativeMethods.cairo_pattern_set_matrix (Handle, value);
			}

			get {
				CheckDisposed ();
				Matrix m = new Matrix ();
				NativeMethods.cairo_pattern_get_matrix (Handle, m);
				return m;
			}
		}

#pragma warning disable 612
		public IntPtr Handle {
			get { return pattern; }
			private set { pattern = value; }
		}
#pragma warning restore 612

		[Obsolete]
		public IntPtr Pointer {
			get { return pattern; }
		}

		public PatternType PatternType {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_pattern_get_type (Handle);
			}
		}
	}
}

