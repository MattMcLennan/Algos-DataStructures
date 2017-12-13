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

    internal class AdjacencyList
    {
        LinkedList<(int, int)>[] adjacencyList;

        public AdjacencyList(int vertices)
        {
            adjacencyList = new LinkedList<(int, int)>[vertices];

            for (var i = 0; i <= adjacencyList.Length - 1; i++)
            {
                adjacencyList[i] = new LinkedList<(int, int)>();
            }
        }

        public void AddEdgeAtEnd(int startVertex, int endVertex, int weight)
        {
            adjacencyList[startVertex].AddLast((endVertex, weight));
        }
 
        public void AddEdgeAtBegin(int startVertex, int endVertex, int weight)
        {
            adjacencyList[startVertex].AddFirst((endVertex, weight));
        }
 
        public int NumberOfVertices()
        {
            return adjacencyList.Length;
        }
 
        public LinkedList<(int, int)> this[int index]
        {
            get
            {
                LinkedList<(int, int)> edgeList = new LinkedList<(int, int)>(adjacencyList[index]);
                return edgeList;
            }
        }
 
        public void PrintAdjacencyList()
        {
            int i = 0;
            foreach (LinkedList<(int, int)> list in adjacencyList)
            {
                Console.Write("adjacencyList[" + i + "] -> ");
 
                foreach ((int, int) edge in list)
                {
                    Console.Write(edge.Item1 + "(" + edge.Item2 + ")");
                }
 
                ++i;
                Console.WriteLine();
            }
        }
 
        public bool RemoveEdge(int startVertex, int endVertex, int weight)
        {
            (int, int) edge = (endVertex, weight);
            return adjacencyList[startVertex].Remove(edge);
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
            AdjacencyList adjacencyList = new AdjacencyList(lineCount + 1);
            foreach (var line in File.ReadLines(@fileName))
            {
                var x = line;
                // first column is vertex
                // rest are edged

            } 

            Console.WriteLine("Enter the edges with weights -");
 
            // 1 - 200 vertices
            // do something with the input

            return (int)ExitCode.Success;
        }
    }
}
