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
	public class FontFace : IDisposable
	{
		IntPtr handle;

		internal static FontFace Lookup (IntPtr handle, bool owner)
		{
			if (handle == IntPtr.Zero)
				return null;
			return new FontFace (handle, owner);
		}

		~FontFace ()
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
				CairoDebug.OnDisposed<FontFace> (handle, disposing);

			if (handle == IntPtr.Zero)
				return;

			NativeMethods.cairo_font_face_destroy (handle);
			handle = IntPtr.Zero;
		}

		void CheckDisposed ()
		{
			if (handle == IntPtr.Zero)
				throw new ObjectDisposedException ("Object has already been disposed");
		}

		public FontFace (IntPtr handle, bool owned)
		{
			if (handle == IntPtr.Zero)
				throw new ArgumentException ("handle should not be NULL", "handle");

			this.handle = handle;
			if (!owned)
				NativeMethods.cairo_font_face_reference (handle);
			if (CairoDebug.Enabled)
				CairoDebug.OnAllocated (handle);
		}

		public IntPtr Handle {
			get {
				return handle;
			}
		}

		public Status Status {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_font_face_status (handle);
			}
		}
		
		public FontType FontType {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_font_face_get_type (handle);
			}
		}

		public uint ReferenceCount {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_font_face_get_reference_count (handle);
			}
		}
	}
}

