using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KargerMinCutGraph
{
    internal enum ExitCode : int
    {
        Success = 0,
        InvalidFileName = 1,
        UnknownError = 10
    }

    internal class Graph
    {
        LinkedList<int>[] adjacencyList;

        public Graph(int vertices)
        {
            adjacencyList = new LinkedList<int>[vertices];

            for (var i = 0; i <= adjacencyList.Length - 1; i++)
            {
                adjacencyList[i] = new LinkedList<int>();
            }
        }

        public void AddEdge(int startVertex, int endVertex)
        {
            // Needed to - 1 to offset 1 based positioning vs array 0 based index
            adjacencyList[startVertex - 1].AddLast((endVertex - 1));
        }
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

            var lineCount = File.ReadLines(@fileName).Count();
            Graph graph = new Graph(lineCount);
            foreach (var line in File.ReadLines(@fileName))
            {
                var info = line.Split('\t').ToArray();
                var vertex = info[0];

                for(var i = 1; i <= info.Length - 2; i++)
                {
                    graph.AddEdge(Convert.ToInt32(vertex), Convert.ToInt32(info[i]));
                }
            } 

            return (int)ExitCode.Success;
        }
    }
}
