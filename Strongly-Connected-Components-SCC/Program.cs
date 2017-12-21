using System;
using System.Collections.Generic;
using System.IO;

namespace Strongly_Connected_Components_SCC
{
    internal class Graph 
    {
        public Graph()
        {
            Vertices = new List<int>();
            Edges = new List<Edge>();
        }

         public List<int> Vertices { get; set; }

        public List<Edge> Edges { get; set; }
   }

    public class Edge
    {
        public int StartPoint { get; set; }

        public int EndPoint { get; set; }
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
                        graph.Vertices.Add(vertex);

                        int endpoint;
                        if (int.TryParse(splited[1], out endpoint))
                        {
                            graph.Edges.Add(new Edge
                            {
                                StartPoint = vertex,
                                EndPoint = endpoint
                            });
                        }
                    }

                    text = reader.ReadLine();
                }
            }

            return graph;
        }
    }
}
