using System;
using System.Collections.Generic;
using System.IO;

namespace InversionIntCount
{
    internal enum ExitCode : int
    {
        Success = 0,
        InvalidFilename = 1,
        UnknownError = 10
    }

    class Program
    {
        static int GetInversions(List<int> inputList)
        {
            var inversions = 0;
            var len = inputList.Count - 1;

            for (var i = 0; i <= len; i++)
            {
                for (var j = i; j <= len; j++)
                {
                    if (inputList[i] > inputList[j])
                    {
                        inversions++;
                    }
                }
            }

            return inversions;
        }

        static int Main(string[] args)
        {
            var fileName = args[0];
            if (!File.Exists(fileName))
            {
                return (int)ExitCode.InvalidFilename;
            }

            var totalInversions = 0;
            foreach(var line in File.ReadLines(@fileName))
            {
                totalInversions += GetInversions(ConvertLine(line));
            }

            Console.WriteLine($"Total Inversions = {totalInversions}");
            return (int)ExitCode.Success;
        }

        private static List<int> ConvertLine(string line)
        {
            var result = new List<int>();
            foreach (char i in line)
            {
                result.Add(i);
            }

            return result;
        }
    }
}
