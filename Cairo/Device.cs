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

	public enum DeviceType {
		Drm,
		GL,
		Script,
		Xcb,
		Xlib,
		Xml,
	}

	public class Device : IDisposable
	{

		IntPtr handle;

		internal Device (IntPtr handle)
		{
			if (handle == IntPtr.Zero)
				throw new ArgumentException ("handle should not be NULL", "handle");

			this.handle = NativeMethods.cairo_device_reference (handle);
		}

		public Status Acquire ()
		{
			CheckDisposed ();
			return NativeMethods.cairo_device_acquire (handle);
		}

		public void Dispose ()
		{
			if (handle != IntPtr.Zero)
				NativeMethods.cairo_device_destroy (handle);
			handle = IntPtr.Zero;
			GC.SuppressFinalize (this);
		}

		void CheckDisposed ()
		{
			if (handle == IntPtr.Zero)
				throw new ObjectDisposedException ("Object has already been disposed");
		}

		public void Finish ()
		{
			CheckDisposed ();
			NativeMethods.cairo_device_finish (handle);
		}

		public void Flush ()
		{
			CheckDisposed ();
			NativeMethods.cairo_device_flush (handle);
		}

		public void Release ()
		{
			CheckDisposed ();
			NativeMethods.cairo_device_release (handle);
		}

		public Status Status {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_device_status (handle);
			}
		}

		public DeviceType Type {
			get {
				CheckDisposed ();
				return NativeMethods.cairo_device_get_type (handle);
			}
		}

	}
}

