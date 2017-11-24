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

        static int QuickSort(List<int> inputList, int left, int right)
        {
            if (left >= right || left < 0 || right < 0)
            {
                return 0;
            }

            var comparisons = right - left - 1;
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
            
            int sortLeft = QuickSort(inputList, left, i - 1);
            int sortRight = QuickSort(inputList, i, right);

            return comparisons + sortLeft + sortRight;
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

            var quickSort = QuickSort(inputIntegers, 0, inputIntegers.Count);
            Console.WriteLine($"Total Comparisons Completed = {quickSort}");
            return (int)ExitCode.Success;
        }
    }
}
