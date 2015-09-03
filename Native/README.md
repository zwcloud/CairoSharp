Cairo Solution for Visual Studio 2015 - README
----------------------------------------------

Please read COPYING for license information. This readme file is for
describing the purpose and use of this solution. Solution is just a fancy
name for "a bunch of projects that might depend on each other" in VS.

What is cairo:
---------------------------------------
"Cairo is a 2D graphics library with support for multiple output devices."
http://cairographics.org/

What is pixman:
---------------------------------------
It is a library that cairo uses, but you can find out more about it on the
cairo website. It is included in the download section of cairo as a
separate package file.

Purpose of this solution:
---------------------------------------
This solution is for building 32bit/64bit pixman and cairo with Visual Studio
2015 without having to install scripting languages and shell environments
that emulate linux, that the original makefiles of cairo need even when
building cairo with Visual Studio under Windows.

How to compile:
---------------------------------------
Download the latest cairo and pixman sources from
http://cairographics.org/releases/
*Please note that this solution does not compile cairomm nor use it
for anything.

1. Unpack the cairo source files to the subfolder cairo and the
pixman sources to pixman

* The packages for cairo/pixman might contain the files inside a folder. If
you only see a folder at the root of the packages, and no makefiles, no
readmes no nothing, only extract the files from that folder and not the
folder itself.

2. Open projects\cairo.sln with visual studio. You can skip to the last
step if you don't need SVG or PNG etc. support, but need to work with
Win32 GDI "surfaces" (HDC) and the basic drawing functionality of cairo.

3.a Edit projects\cairo\src\cairo-features.h to change what
additional features you need, or comment out what you don't want. For
example if you don't need to work with win32 GDI "surfaces" (HDC) you can
comment out the define for CAIRO_HAS_WIN32_SURFACE.
*Please note that if you want some features you might have to supply zlib
and/or libpng and/or freetype beside pixman/cairo, just unzip it to the folders. 
You will also need to compile static libs of those.

3.b The files for the unused features are included in the solution, but
disabled. Enable what you need in the Solution Explorer in VS by selecting
the files and right-click/properties and erase the "Yes" string from
"Excluded From Build".

4. If you want to create a DLL instead of the static libraries this solution
creates, you can change that in the project settings with the VS interface.
You might have to edit the cairo project properties and add additional
libraries and their paths if you use features that require it. If you need
help with that, please learn to use the user interface of Visual Studio.

* This solution creates static libraries with the platform toolset for
vs2015.

5. Compile and link the resulting static libraries (both cairo and pixman)
with your project. *

* When using cairo as a static library you will have to specify the
CAIRO_WIN32_STATIC_BUILD preprocessor directive in YOUR OWN PROJECT THAT
USES cairo, or define it before including the cairo header files.

Original project's sourceforge page at:
https://sourceforge.net/projects/cairosolutionvs2012
