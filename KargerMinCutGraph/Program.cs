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

        public void RemoveEdge(int startVertex, int endVertex)
        {
            MergeVertices(startVertex, endVertex);
            RemoveVertex(endVertex);
            RemoveReferenceToVertex(endVertex);
        }

        private void MergeVertices(int startVertex, int endVertex)
        {
            foreach (var item in adjacencyList[endVertex])
            {
                if (!adjacencyList[startVertex].Contains(item))
                {
                    adjacencyList[startVertex].AddLast(item);
                }
            }
        }

        private void RemoveReferenceToVertex(int endVertex)
        {
            foreach (var item in adjacencyList)
            {
                if (item.Contains(endVertex))
                {
                    item.Remove(endVertex);
                }
            }
        }

        public void RemoveVertex(int vertex)
        {
            adjacencyList[vertex] = new LinkedList<int>();
        }

        public int CountOfEdges(int vertex)
        {
            return adjacencyList[vertex].Count;
        }

        public int CountOfVertices()
        {
            int total = 0;

            foreach (var item in adjacencyList)
            {
                if (item.Any())
                {
                    total++;
                }
            }

            return total;
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

            int minCut = GetMinCutFromGraph(graph);
            Console.WriteLine(minCut);

            return (int)ExitCode.Success;
        }

        private static int GetMinCutFromGraph(Graph graph)
        {
            if (graph.CountOfVertices() <= 2)
            {
                return 1;
            }

            Random random = new Random();
            int randomVertex = 0;
            do 
            {
                randomVertex = random.Next(graph.CountOfVertices());
            }
            while(graph.CountOfEdges(randomVertex) == 0);

            int randomEdge = random.Next(0, graph.CountOfEdges(randomVertex));

            graph.RemoveEdge(randomVertex, randomEdge);

            return GetMinCutFromGraph(graph);
        }
    }
}