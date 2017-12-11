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
using System.Collections;

namespace Cairo {

	public class Surface : IDisposable
	{
		IntPtr handle = IntPtr.Zero;

		protected Surface (IntPtr handle, bool owner)
		{
			if (handle == IntPtr.Zero)
				throw new ArgumentException ("handle should not be NULL", "handle");

			this.handle = handle;
			if (!owner)
				NativeMethods.cairo_surface_reference (handle);
			if (CairoDebug.Enabled)
				CairoDebug.OnAllocated (handle);
		}

		public static Surface Lookup (IntPtr surface, bool owned)
		{
			SurfaceType st = NativeMethods.cairo_surface_get_type (surface);
			switch (st) {
			case SurfaceType.Image:
				return new ImageSurface (surface, owned);
			case SurfaceType.Xlib:
				return new XlibSurface (surface, owned);
			case SurfaceType.Xcb:
				return new XcbSurface (surface, owned);
			case SurfaceType.Glitz:
				return new GlitzSurface (surface, owned);
			case SurfaceType.Win32:
				return new Win32Surface (surface, owned);
			case SurfaceType.Pdf:
				return new PdfSurface (surface, owned);
			case SurfaceType.PS:
				return new PSSurface (surface, owned);
			case SurfaceType.DirectFB:
				return new DirectFBSurface (surface, owned);
			case SurfaceType.Svg:
				return new SvgSurface (surface, owned);
			default:
				return new Surface (surface, owned);
			}
		}

		[Obsolete ("Use an ImageSurface constructor instead.")]
		public static Cairo.Surface CreateForImage (
			ref byte[] data, Cairo.Format format, int width, int height, int stride)
		{
			IntPtr p = NativeMethods.cairo_image_surface_create_for_data (
				data, format, width, height, stride);

			return new Cairo.Surface (p, true);
		}

		[Obsolete ("Use an ImageSurface constructor instead.")]
		public static Cairo.Surface CreateForImage (
			Cairo.Format format, int width, int height)
		{
			IntPtr p = NativeMethods.cairo_image_surface_create (
				format, width, height);

			return new Cairo.Surface (p, true);
		}


		public Cairo.Surface CreateSimilar (
			Cairo.Content content, int width, int height)
		{
			IntPtr p = NativeMethods.cairo_surface_create_similar (
				this.Handle, content, width, height);

			return new Cairo.Surface (p, true);
		}

		~Surface ()
		{
			Dispose (false);
		}

		//[Obsolete ("Use Context.SetSource() followed by Context.Paint()")]
		public void Show (Context gr, double x, double y)
		{
			NativeMethods.cairo_set_source_surface (gr.Handle, handle, x, y);
			NativeMethods.cairo_paint (gr.Handle);
		}

		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		protected virtual void Dispose (bool disposing)
		{
			if (!disposing || CairoDebug.Enabled)
				CairoDebug.OnDisposed<Surface> (handle, disposing);

			if (handle == IntPtr.Zero)
				return;

			NativeMethods.cairo_surface_destroy (handle);
			handle = IntPtr.Zero;
		}

		protected void CheckDisposed ()
		{
			if (handle == IntPtr.Zero)
				throw new ObjectDisposedException ("Object has already been disposed");
		}

		public Status Finish ()
		{
			CheckDisposed ();
			NativeMethods.cairo_surface_finish (handle);
			return Status;
		}

		public void Flush ()
		{
			CheckDisposed ();
			NativeMethods.cairo_surface_flush (handle);
		}

		public void MarkDirty ()
		{
			CheckDisposed ();
			NativeMethods.cairo_surface_mark_dirty (Handle);
		}

		public void MarkDirty (Rectangle rectangle)
		{
			CheckDisposed ();
			NativeMethods.cairo_surface_mark_dirty_rectangle (Handle, (int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height);
		}

		public IntPtr Handle {
			get {
				return handle;
			}
		}

		public PointD DeviceOffset {
			get {
				CheckDisposed ();
				double x, y;
				NativeMethods.cairo_surface_get_device_offset (handle, out x, out y);
				return new PointD (x, y);
			}

			set {
				CheckDisposed ();
				NativeMethods.cairo_surface_set_device_offset (handle, value.X, value.Y);
			}
		}

		[Obsolete ("Use Dispose()")]
		public void Destroy()
		{
			Dispose ();
		}

		public void SetFallbackResolution (double x, double y)
		{
			CheckDisposed ();
			NativeMethods.cairo_surface_set_fallback_resolution (handle, x, y);
		}

		public void WriteToPng (string filename)
		{
			CheckDisposed ();
			NativeMethods.cairo_surface_write_to_png (handle, filename);
		}

		[Obsolete ("Use Handle instead.")]
		public IntPtr Pointer {
			get {
				return handle;
			}
		}

		public Status Status {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_surface_status (handle);
			}
		}

		public Content Content {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_surface_get_content (handle);
			}
		}

		public SurfaceType SurfaceType {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_surface_get_type (handle);
			}
		}

		public uint ReferenceCount {
			get {
				return NativeMethods.cairo_surface_get_reference_count (handle); }
		}
	}
}
