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
using Cairo;

namespace Cairo {

	public class Path : IDisposable
	{
		IntPtr handle = IntPtr.Zero;

		internal Path (IntPtr handle)
		{
			if (handle == IntPtr.Zero)
				throw new ArgumentException ("handle should not be NULL", "handle");

			this.handle = handle;
			if (CairoDebug.Enabled)
				CairoDebug.OnAllocated (handle);
		}

		~Path ()
		{
			Dispose (false);
		}

		public IntPtr Handle { get { return handle; } }

		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		protected virtual void Dispose (bool disposing)
		{
			if (!disposing || CairoDebug.Enabled)
				CairoDebug.OnDisposed<Path> (handle, disposing);

			if (handle == IntPtr.Zero)
				return;

			NativeMethods.cairo_path_destroy (handle);
			handle = IntPtr.Zero;
		}
	}
}
