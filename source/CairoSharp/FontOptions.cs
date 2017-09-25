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
	public class FontOptions : IDisposable
	{
		IntPtr handle;

		public FontOptions () : this (NativeMethods.cairo_font_options_create ())
		{
		}

		~FontOptions ()
		{
			Dispose (false);
		}

		internal FontOptions (IntPtr handle)
		{
			if (handle == IntPtr.Zero)
				throw new ArgumentException ("handle should not be NULL", "handle");

			this.handle = handle;
			if (CairoDebug.Enabled)
				CairoDebug.OnAllocated (handle);
		}

		public FontOptions Copy ()
		{
			CheckDisposed ();
			return new FontOptions (NativeMethods.cairo_font_options_copy (handle));
		}

		[Obsolete ("Use Dispose()")]
		public void Destroy ()
		{
			Dispose ();
		}

		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		protected virtual void Dispose (bool disposing)
		{
			if (!disposing || CairoDebug.Enabled)
				CairoDebug.OnDisposed<FontOptions> (handle, disposing);

			if (handle == IntPtr.Zero)
				return;

			NativeMethods.cairo_font_options_destroy (handle);
			handle = IntPtr.Zero;
		}

		void CheckDisposed ()
		{
			if (handle == IntPtr.Zero)
				throw new ObjectDisposedException ("Object has already been disposed");
		}

		public static bool operator == (FontOptions options, FontOptions other)
		{
			return Equals (options, other);
		}

		public static bool operator != (FontOptions options, FontOptions other)
		{
			return !(options == other);
		}

		public override bool Equals (object other)
		{
			return Equals (other as FontOptions);
		}

		bool Equals (FontOptions options)
		{
			return options != null && NativeMethods.cairo_font_options_equal (Handle, options.Handle);
		}

		public IntPtr Handle {
			get { return handle; }
		}

		public override int GetHashCode ()
		{
			return (int) NativeMethods.cairo_font_options_hash (handle);
		}
		
		public void Merge (FontOptions other)
		{
			if (other == null)
				throw new ArgumentNullException ("other");
			CheckDisposed ();
			NativeMethods.cairo_font_options_merge (handle, other.Handle);
		}

		public Antialias Antialias {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_font_options_get_antialias (handle);
			}
			set {
				CheckDisposed ();
				NativeMethods.cairo_font_options_set_antialias (handle, value);
			}
		}

		public HintMetrics HintMetrics {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_font_options_get_hint_metrics (handle);
			}
			set {
				CheckDisposed ();
				NativeMethods.cairo_font_options_set_hint_metrics (handle, value);
			}
		}

		public HintStyle HintStyle {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_font_options_get_hint_style (handle);
			}
			set {
				CheckDisposed ();
				NativeMethods.cairo_font_options_set_hint_style (handle, value);
			}
		}

		public Status Status {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_font_options_status (handle);
			}
		}

		public SubpixelOrder SubpixelOrder {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_font_options_get_subpixel_order (handle);
			}
			set {
				CheckDisposed ();
				NativeMethods.cairo_font_options_set_subpixel_order (handle, value);
			}
		}
	}
}

