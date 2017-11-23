using System;
using System.Collections.Generic;
using System.IO;

namespace QuickSort
{
    internal enum ExitCode : int
    {
        Success = 0,
        InvalidFilename = 1,
        UnknownError = 10
    }

    class Program
    {
        static (int totalComparisons, List<int> list) QuickSort(List<int> list)
        {


            return (0, new List<int>());
        }

        static int Main(string[] args)
        {
            var fileName = args[0];
            if (!File.Exists(fileName))
            {
                return (int)ExitCode.InvalidFilename;
            }

            List<int> inputIntegers = new List<int>();
            foreach (var line in File.ReadLines(@fileName))
            {
                inputIntegers.Add(Convert.ToInt32(line));
            }

            var quickSort = QuickSort(inputIntegers);
            Console.WriteLine($"Total Comparisons Completed = {quickSort.totalComparisons}");
            return (int)ExitCode.Success;
        }
    }
}
