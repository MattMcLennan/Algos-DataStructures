﻿using System;
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
        static int ChoosePivot(List<int> inputList)
        {
            return 0;
        }

        private static void Swap(List<int> inputList, int index1, int index2)
        {
            var temp = inputList[index1];
            inputList[index1] = inputList[index2];
            inputList[index2] = temp;
        }

        static void PartitionInputAroundPivot(int pivotIndex, List<int> inputList)
        {
            var pivot = inputList[pivotIndex];
            var i = pivotIndex + 1;

            for (int j = pivotIndex + 1; j < inputList.Count - 1; j++)
            {
                if (inputList[j] < pivot)
                {
                    Swap(inputList, j, i);
                    i++;
                }
            }

            Swap(inputList, pivotIndex, i - 1);
        }

        static (int totalComparisons, List<int> list) QuickSort(List<int> inputList, int comparisonRunningTotal)
        {
            if (inputList.Count <= 1)
            {
                return (0, new List<int>());
            }

            int pivotIndex = ChoosePivot(inputList);
            PartitionInputAroundPivot(pivotIndex, inputList);

            var leftSubList = inputList.GetRange(0, pivotIndex);
            var rightSubList = inputList.GetRange(pivotIndex + 1, inputList.Count - leftSubList.Count - 1);
            var sortLeft = QuickSort(leftSubList, leftSubList.Count);
            var sortRight = QuickSort(rightSubList, rightSubList.Count);

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

            var quickSort = QuickSort(inputIntegers, 0);
            Console.WriteLine($"The sorted list is: {quickSort.list}");
            Console.WriteLine($"Total Comparisons Completed = {quickSort.totalComparisons}");
            return (int)ExitCode.Success;
        }
    }
}
