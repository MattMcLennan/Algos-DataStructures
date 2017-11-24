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
        static int ChoosePivotIndex(List<int> inputList)
        {
            return 0; 
        }

        private static void Swap(List<int> inputList, int index1, int index2)
        {
            var temp = inputList[index1];
            inputList[index1] = inputList[index2];
            inputList[index2] = temp;
        }

        static int PartitionInputAroundPivot(List<int> inputList, int left, int right)
        {
            if (left > right)
            {
                return -1;
            }

            var pivot = inputList[left];

            var i = left + 1;
            for (var j = left + 1; j <= right; j++)
            {
                if (inputList[j] < pivot)
                {
                    Swap(inputList, j, i);
                    i++;
                }
            }

            Swap(inputList, left, i - 1);
            return left;
        }

        static (int totalComparisons, List<int> list) QuickSort(List<int> inputList, int comparisonRunningTotal, int left, int right)
        {
            if (left >= right || left < 0 || right < 0)
            {
                return (comparisonRunningTotal, inputList);
            }

            // int index = PartitionInputAroundPivot(inputList, ChoosePivotIndex(inputList), right);

            var pivot = inputList[left];

            var i = left + 1;
            var j = 0;
            for (j = left + 1; j < right; j++)
            {
                if (inputList[j] < pivot)
                {
                    Swap(inputList, j, i);
                    i++;
                }
            }

            Swap(inputList, left, i - 1);
            
            (int totalComparisons, List<int> list) sortLeft = QuickSort(inputList, comparisonRunningTotal + left - 1, left, i - 1);
            (int totalComparisons, List<int> list) sortRight = QuickSort(inputList, comparisonRunningTotal + right - 1, i, right);

            return (sortLeft.totalComparisons + sortRight.totalComparisons, inputList);
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

            var quickSort = QuickSort(inputIntegers, 0, 0, inputIntegers.Count);
            Console.WriteLine($"The sorted list is: {quickSort.list}");
            Console.WriteLine($"Total Comparisons Completed = {quickSort.totalComparisons}");
            return (int)ExitCode.Success;
        }
    }
}
