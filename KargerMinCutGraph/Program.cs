using System;
using System.IO;

namespace KargerMinCutGraph
{
    internal enum ExitCode : int
    {
        Success = 0,
        InvalidFileName = 1,
        UnknownError = 10
    }

    class Program
    {
        static int Main(string[] args)
        {
            var fileName = args[0];
            if (!File.Exists(fileName))
            {
                return (int)ExitCode.InvalidFileName;
            }

            // do something with the input

            return (int)ExitCode.Success;
        }
    }
}
