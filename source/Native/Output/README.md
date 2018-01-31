Projects will out put static libraries into *Output/lib* and dll(as well as its import lib and pdb file) into *Output/bin*.

Don't run NativeLibraryCopier.exe directly. It is used in the pre-build event of CairoSharp project to copy native dll and pdb files to proper places.