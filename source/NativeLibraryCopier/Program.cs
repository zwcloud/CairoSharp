using System;

namespace NativeLibraryCopier
{
    class Program
    {
        enum ExitCode : int
        {
            Success = 0,
            InvalidArguments = 1,
            NativeProjectNotBuilt = 2,
        }

        static int Main(string[] args)
        {
            if (args.Length != 4)
            {
                PrintUsage();
                return (int)ExitCode.InvalidArguments;
            }

            var SolutionDir = args[0];
            var ConfigurationName = args[1];
            var PlatformName = args[2];
            var TargetDir = args[3];

            var CairoDllFileName = "cairod.dll";
            var CairoPdbFileName = "cairod.pdb";
            
            if (ConfigurationName == "Release")
            {
                CairoDllFileName = "cairo.dll";
                CairoPdbFileName = "cairo.pdb";
            }
            var PlatformTag = "x64";
            if (PlatformName == "x86")
            {
                PlatformTag = "x86";
            }
            //PlatformTag of `AnyCPU` is x64 by default.

            var DllFilePath = string.Format($@"{SolutionDir}source\Native\Output\bin\win-{PlatformTag}\{CairoDllFileName}");
            var PdbFilePath = string.Format($@"{SolutionDir}source\Native\Output\bin\win-{PlatformTag}\{CairoPdbFileName}");
            if (!System.IO.File.Exists(DllFilePath))
            {
                Console.WriteLine($"ERROR: Native Cairo dll built for {ConfigurationName}/{PlatformName} not found. Please build source/native/cairo first.");
                return (int)ExitCode.NativeProjectNotBuilt;
            }
            else
            {
                System.IO.File.Copy(DllFilePath, $"{TargetDir}{CairoDllFileName}", true);
                System.IO.File.Copy(PdbFilePath, $"{TargetDir}{CairoPdbFileName}", true);
            }

            return (int)ExitCode.Success;
        }

        private static void PrintUsage()
        {
            Console.WriteLine(@"Usage:
NativeLibraryCopier.exe <SolutionDir> <ConfigurationName> <PlatformName> <TargetDir>");
        }
    }
}
