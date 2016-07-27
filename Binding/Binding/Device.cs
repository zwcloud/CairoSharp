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

