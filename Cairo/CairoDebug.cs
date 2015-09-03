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
