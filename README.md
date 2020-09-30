[![NuGet](https://img.shields.io/nuget/v/SharpCairo.svg?style=flat-square)](https://www.nuget.org/packages/SharpCairo/)

# CairoSharp
CairoSharp is a C# wrapper for .NET Standard 1.6/2.0 of [cairo](https://www.cairographics.org/). All dependencies included.

Cairo is a 2D graphics library with support for multiple output devices.

Cairo is designed to produce consistent output on all output media while taking advantage of display hardware acceleration when available.
The cairo API provides operations similar to the drawing operators of PostScript and PDF.
Operations in cairo including stroking and filling cubic Bézier splines, transforming and compositing translucent images, and antialiased text rendering.
All drawing operations can be transformed by any affine transformation (scale, rotation, shear, etc.).

# Platforms

[A former release](https://github.com/zwcloud/CairoSharp/releases/tag/dotnet45_v1) works on Windows desktop (Winform/WPF/Console Application) on .NET 4.5 and linux (tested on Ubuntu 12.04) on mono 4.2. CairoSharp was ported to .NET Core (.NET Standard 1.6) after that release.

Supported platforms:  

* windows 32bit / 64bit
* linux 64bit (tested on Ubuntu 16.04-server)

Other platforms that support .net core may work, but have not been tested.

On Linux _libcairo_ is a prerequisite. `sudo apt-get update && sudo apt-get install -y libcairo2`

# [Documentation](https://github.com/zwcloud/CairoSharp/wiki)

# Build

## Windows

### Build native dlls
 We use VS2019 to build native libraries as dll files.

* Preparation:

	1. Open Visual Studio Installer, click `Modify` or `More/Modify`;
	2. Check __Desktop development with C++__;
	3. In the __Summary__ section on the right, open __Desktop development with C++__, then check __Windows 8.1 SDK and UCRT SDK__.
	4. Click `Modify` button.

* Build
	
	1. Open CarioSharp.sln with VS2019.
	2. Select a configuration: `Debug` or `Release`, and a platform: `x86` or `x64`. For `AnyCPU`, be sure to set/unset [Prefer 32-bit](https://stackoverflow.com/questions/13149905/any-cpu-prefer-32-bit) to use the same architecture as the native dll.
	3. Find the project `source/native/cairo`, right click and select `Build`.
	4. Built native dll files will be output to `source/native/cairo/bin/`.

#### Build C# projects

* Build CairoSharp

	In VS2019, right click the CairoSharp project then select `Build`. CairoSharp.dll and its native dependencies will be placed in the output dir such as `source\CairoSharp\bin\Debug\netstandard2.0`.

* Build C# demos

	In VS2019, right click a C# demo project then select `Build`. The built demo app, CairoSharp.dll and native dependencies will be placed in the output dir such as `demos\CairoDemo\bin\Debug\netcoreapp2.0`.

**Common Errors:**

> Error C1083 Cannot open include file: 'assert.h': No such file or directory

Make sure `Windows 8.1 SDK and UCRT SDK` is checked (See _Preparation 3_ above). See also [this post](https://stackoverflow.com/a/48483877/3427520).

# Copying/License
__LGPLv3__  
Project CairoSharp (not the native cairo lib but the C# one) is licensed under the LGPL v3 license.

    CairoSharp, A C# wrapper of cairo which is a 2D vector rendering library
    Copyright (C) 2017 gfoidl
    Copyright (C) 2015-2020 Zou Wei, zwcloud@hotmail.com, https://zwcloud.net
    Copyright (C) 2007-2015 Xamarin, Inc.
    Copyright (C) 2006 Alp Toker
    Copyright (C) 2005 John Luke
    Copyright (C) 2004 Novell, Inc (http://www.novell.com)
    Copyright (C) Ximian, Inc. 2003

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

The C# code files in CairoSharp project was taken from Mono/[GTK#](https://github.com/mono/gtk-sharp/tree/master/cairo)(Version 3.0.0), licensed under the GNU LGPL. So CairoSharp uses LGPL as well.

## Native libraries

The [Native project files](https://github.com/zwcloud/CairoSharp/tree/master/Native/projects) is generated according to a VS2015 Solution from [Cairo-VS](https://github.com/DomAmato/Cairo-VS).

* [cairo](http://www.cairographics.org/)
  Version 1.17.2
  [COPYING Info](https://github.com/zwcloud/CairoSharp/blob/master/Native/cairo/COPYING)

* [libpng](http://libmng.com/pub/png/libpng.html)
  Version 1.6.18
  [COPYING Info](https://github.com/zwcloud/CairoSharp/blob/master/Native/libpng/LICENSE)

* [pixman](http://www.pixman.org/) 
  Version 0.36.0
  [COPYING Info](https://github.com/zwcloud/CairoSharp/blob/master/Native/pixman/COPYING)

* [zlib](http://www.zlib.net/)
  Version 1.2.8
  [COPYING Info](https://github.com/zwcloud/CairoSharp/blob/master/Native/zlib/README)

* [freetype](http://www.freetype.org/)
  Version 2.6
  [COPYING Info](https://github.com/zwcloud/CairoSharp/blob/master/Native/freetype/docs/LICENSE.TXT)

These libraries will be updated if new verisons are released.  
Last update date of these Native libraries: 2020-09-30
