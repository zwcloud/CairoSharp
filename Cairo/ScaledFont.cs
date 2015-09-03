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
   
	public class ScaledFont : IDisposable
	{
		protected IntPtr handle = IntPtr.Zero;

		internal ScaledFont (IntPtr handle, bool owner)
		{
			if (handle == IntPtr.Zero)
				throw new ArgumentException ("handle should not be NULL", "handle");

			this.handle = handle;
			if (!owner)
				NativeMethods.cairo_scaled_font_reference (handle);
			if (CairoDebug.Enabled)
				CairoDebug.OnAllocated (handle);
		}

		public ScaledFont (FontFace fontFace, Matrix matrix, Matrix ctm, FontOptions options)
			: this (NativeMethods.cairo_scaled_font_create (fontFace.Handle, matrix, ctm, options.Handle), true)
		{
		}

		~ScaledFont ()
		{
			Dispose (false);
		}

		public IntPtr Handle {
			get {
				return handle;
			}
		}

		public FontExtents FontExtents {
			get {
				CheckDisposed ();
				FontExtents extents;
				NativeMethods.cairo_scaled_font_extents (handle, out extents);
				return extents;
			}
		}

		public Matrix FontMatrix {
			get {
				CheckDisposed ();
				Matrix m;
				NativeMethods.cairo_scaled_font_get_font_matrix (handle, out m);
				return m;
			}
		}

		public FontType FontType {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_scaled_font_get_type (handle);
			}
		}

		public TextExtents GlyphExtents (Glyph[] glyphs)
		{
			CheckDisposed ();
			IntPtr ptr = Context.FromGlyphToUnManagedMemory (glyphs);
			TextExtents extents;

			NativeMethods.cairo_scaled_font_glyph_extents (handle, ptr, glyphs.Length, out extents);

			Marshal.FreeHGlobal (ptr);
			return extents;
		}
	
		public Status Status
		{
			get {
				CheckDisposed ();
				return NativeMethods.cairo_scaled_font_status (handle);
			}
		}

		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		protected virtual void Dispose (bool disposing)
		{
			if (!disposing || CairoDebug.Enabled)
				CairoDebug.OnDisposed<ScaledFont> (handle, disposing);

			if (handle == IntPtr.Zero)
				return;

			NativeMethods.cairo_scaled_font_destroy (handle);
			handle = IntPtr.Zero;
		}

		void CheckDisposed ()
		{
			if (handle == IntPtr.Zero)
				throw new ObjectDisposedException ("Object has already been disposed");
		}

		[Obsolete]
		protected void Reference ()
		{
			CheckDisposed ();
			NativeMethods.cairo_scaled_font_reference (handle);
		}
	}
}

