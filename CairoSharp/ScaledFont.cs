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
	}
}

