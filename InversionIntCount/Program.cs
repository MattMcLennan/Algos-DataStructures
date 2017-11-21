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

        static int GetInversions2(List<int> integerList)
        {
            List<int> left = new List<int>();
            List<int> right = new List<int>();

            int len = integerList.Count / 2;
            left.AddRange(integerList.GetRange(0, len));
            right.AddRange(integerList.GetRange(len, integerList.Count - len));

            if (left.Count == 1 || right.Count == 1)
            {
                int leftIndex = 0;
                int rightIndex = 0;
                int inversions = 0;

                while(leftIndex < left.Count && rightIndex < right.Count)
                {
                    if (left[leftIndex] > right[rightIndex])
                    {
                        inversions++;
                        leftIndex++;
                    }
                    else 
                    {
                        rightIndex++;
                    }
                }

                inversions += right.Count - rightIndex;
                return inversions;
            }

            return GetInversions2(left) + GetInversions2(right);
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
                totalInversions += GetInversions2(ConvertLine(line));
            }

            Console.WriteLine($"Total Inversions = {totalInversions}");
            return (int)ExitCode.Success;
        }

        private static List<int> ConvertLine(string line)
        {
            var result = new List<int>();
            foreach (char i in line)
            {
                result.Add(Convert.ToInt32(Char.GetNumericValue(i)));
            }

            return result;
        }
    }
}
