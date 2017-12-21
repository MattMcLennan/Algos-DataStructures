using System;
using System.Collections.Generic;
using System.IO;

namespace Strongly_Connected_Components_SCC
{
    internal class Graph 
    {
        
        public Graph()
        {
            Data = new Dictionary<int, LinkedList<int>>();
        }

        public void AddOrUpdateEdges(int startingVertex, int endingVertex)
        {
            LinkedList<int> value;
            if (Data.TryGetValue(startingVertex, out value))
            {
                value.AddLast(endingVertex);
                return;
            }

            var endingVertices = new LinkedList<int>();
            endingVertices.AddFirst(endingVertex);
            Data.Add(startingVertex, endingVertices);
        }

        public Dictionary<int, LinkedList<int>> Data { get; set; }
   }

    class Program
    {
        static void Main(string[] args)
        {
            var data = ExtractDataFromFile("TestFile.txt");
        }

        static Graph ExtractDataFromFile(string filePath)
        {
            Graph graph = new Graph();

            using(TextReader reader = File.OpenText(@filePath))
            {
                string text = reader.ReadLine();
                while(text != null)
                {
                    string[] splited = text.Split(' ');

                    int vertex;
                    if (int.TryParse(splited[0], out vertex))
                    {
                        int endpoint;
                        if (int.TryParse(splited[1], out endpoint))
                        {
                            graph.AddOrUpdateEdges(vertex, endpoint);
                        }
                    }

                    text = reader.ReadLine();
                }
            }

            return graph;
        }
    }
}
