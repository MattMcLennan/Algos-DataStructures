using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        static (long count, List<int> outputList) CountAndSort(List<int> integerList)
        {
            if (integerList.Count <= 1)
            {
                return (0, integerList);
            }

            int mid = integerList.Count / 2;
            List<int> leftHalf = integerList.GetRange(0, mid);
            List<int> rightHalf = integerList.GetRange(mid, integerList.Count - leftHalf.Count);

            var leftHalfOutput = CountAndSort(leftHalf);
            var rightHalfOutput = CountAndSort(rightHalf);
            (long count, List<int> outputList) mergeOutput = MergeAndCountInversions(leftHalfOutput.outputList, rightHalfOutput.outputList);

            return (leftHalfOutput.count + rightHalfOutput.count + mergeOutput.count, mergeOutput.outputList);
        }

        static (long, List<int>) MergeAndCountInversions(List<int> leftHalf, List<int> rightHalf)
        {
            int leftIndex = 0;
            int rightIndex = 0;
            int inversions = 0;
            var outputList = new List<int>();

            while (leftIndex < leftHalf.Count && rightIndex < rightHalf.Count)
            {
                if (leftHalf[leftIndex] <= rightHalf[rightIndex])
                {
                    outputList.Add(leftHalf[leftIndex]);
                    leftIndex++;
                }
                else
                {
                    outputList.Add(rightHalf[rightIndex]);
                    rightIndex++;
                    inversions += (leftHalf.Count - leftIndex);
                }
            }

            if (leftIndex < leftHalf.Count)
            {
                outputList.AddRange(leftHalf.GetRange(leftIndex, leftHalf.Count - leftIndex));
            }

            if (rightIndex < rightHalf.Count)
            {
                outputList.AddRange(rightHalf.GetRange(rightIndex, rightHalf.Count - rightIndex));
            }

            return (inversions, outputList);
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

            var totalInversions = CountAndSort(inputIntegers);
            Console.WriteLine($"Total Inversions = {totalInversions.count}");
            return (int)ExitCode.Success;
        }
    }
}
