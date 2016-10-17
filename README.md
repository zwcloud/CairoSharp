# CairoSharp
A C# wrapper of cairo. All its dependcies included.

Cairo is a 2D graphics library with support for multiple output devices. Currently supported output targets include the X Window System (via both Xlib and XCB), Win32, image buffers.

# Platforms
Cairo only works on desktop platforms, and so does CarioSharp.

* Desktop
  - Windows
  
    Platform                        | Supported 
    --------------------------------|-----------
    Console Application             | Yes       
    Winform                         | Yes       
    WPF                             | Yes       
    Universal Windows Platform (UWP)| No        

  - Linux
  
    Yes (tested for an older version).

  - macOS
  
    Unknown, not tested. The former Mono.Cairo should work on macOS, so it is very likely that CairoSharp also works.
  
* Mobile
  - Windows
  
    Platform                        | Supported 
    --------------------------------|-----------
    Windows Phone 8.1               | No         
    Universal Windows Platform (UWP)| No        

  - iOS
  
    Unknown, not tested.
  
  - Android
  
    Unknown, not tested.

  __Note__
  Cairo won't compile on UWP or Windows Phone platforms. Because cairo(native) and its dependencies use some c runtime functions and Win32 APIs that are incompatible with the Windows Runtime apps, such as [GradientFill](https://msdn.microsoft.com/en-us/library/dd144957.aspx), which is desktop apps only.

  __Note__
  The cairo-gl backend won't compile on all windows platforms: [`wglGetProcAddress` issue (2016 October)](https://lists.cairographics.org/archives/cairo/2016-October/027774.html), [`wglGetProcAddress` issue (2013 April)](https://lists.cairographics.org/archives/cairo/2013-April/024201.html)

# [Documentation](https://github.com/zwcloud/CairoSharp/wiki)

# Copying/License
__LGPLv3__  
Project Cairo(not the native cairo lib but the C# one) is licensed under the LGPL v3 license.

    CairoSharp, A C# wrapper of cairo which is a 2D vector rendering library
    Copyright (C) 2015-2016  Zou Wei, zwcloud@yeah.net, http://zwcloud.net

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.

The C# code files in Cairo project was taken from Mono/[GTK#](https://github.com/mono/gtk-sharp/tree/master/cairo)(Version 3.0.0), licensed under the GNU LGPL. Copying info:
    
    Copyright (C) 2007-2015  Xamarin, Inc.
    Copyright (C) 2006 Alp Toker
    Copyright (C) 2005 John Luke
    Copyright (C) 2004 Novell, Inc (http://www.novell.com)
    Copyright (C) Ximian, Inc. 2003

## Native libraries

The [Native project files](https://github.com/zwcloud/CairoSharp/tree/master/Native/projects) is generated according to a VS2015 Solution from [Cairo-VS](https://github.com/DomAmato/Cairo-VS).

* [cairo](http://www.cairographics.org/)
  Version 1.15.2
  [COPYING Info](https://github.com/zwcloud/CairoSharp/blob/master/Native/cairo/COPYING)

* [libpng](http://libmng.com/pub/png/libpng.html)
  Version 1.6.18
  [COPYING Info](https://github.com/zwcloud/CairoSharp/blob/master/Native/libpng/LICENSE)

* [pixman](http://www.pixman.org/) 
  Version 0.32.6
  [COPYING Info](https://github.com/zwcloud/CairoSharp/blob/master/Native/pixman/COPYING)

* [zlib](http://www.zlib.net/)
  Version 1.2.8
  [COPYING Info](https://github.com/zwcloud/CairoSharp/blob/master/Native/zlib/README)

* [freetype](http://www.freetype.org/)
  Version 2.6
  [COPYING Info](https://github.com/zwcloud/CairoSharp/blob/master/Native/freetype/docs/LICENSE.TXT)

I will keep this libraries updated if new verison are released.<br/>
Last update time of these Native libraries: 2016/10/17
