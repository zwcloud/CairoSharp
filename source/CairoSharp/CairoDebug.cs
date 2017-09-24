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

namespace Cairo {

	static class CairoDebug
	{
		static System.Collections.Generic.Dictionary<IntPtr,string> traces;

		public static readonly bool Enabled;

		static CairoDebug ()
		{
			var dbg = Environment.GetEnvironmentVariable ("MONO_CAIRO_DEBUG_DISPOSE");
			if (dbg == null)
				return;
			Enabled = true;
			traces = new System.Collections.Generic.Dictionary<IntPtr,string> ();
		}

		public static void OnAllocated (IntPtr obj)
		{
			if (!Enabled)
				throw new InvalidOperationException ();

			traces[obj] = Environment.StackTrace;
		}

		public static void OnDisposed<T> (IntPtr obj, bool disposing)
		{
			if (disposing && !Enabled)
				throw new InvalidOperationException ();

			if (Environment.HasShutdownStarted)
				return;

			if (!disposing) {
				Console.Error.WriteLine ("{0} is leaking, programmer is missing a call to Dispose", typeof(T).FullName);
				if (Enabled) {
					string val;
					if (traces.TryGetValue (obj, out val)) {
						Console.Error.WriteLine ("Allocated from:");
						Console.Error.WriteLine (val);
					}
				} else {
					Console.Error.WriteLine ("Set MONO_CAIRO_DEBUG_DISPOSE to track allocation traces");
				}
			}

			if (Enabled)
				traces.Remove (obj);
		}
	}

}
