using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KargerMinCut
{
    internal enum ExitCode : int
    {
        Success = 0,

        InvalidFileName = 1,

        UnknownError = 10
    }

    public class Edge
    {
        public int StartPoint { get; set; }

        public int EndPoint { get; set; }
    }

    public class Graph : ICloneable
    {
        public List<int> Vertices = new List<int>();

        public List<Edge> Edges = new List<Edge>();

        public object Clone()
        {
            return new Graph
            {
                Edges = new List<Edge>(Edges),
                Vertices = new List<int>(Vertices)
            };
        }
    }

    public class KargerMinCut
    {
        public int MinCut(Graph graph)
        {
            return RemoveVertex(graph.Clone()) / 2;
        }

        private int RemoveVertex(object clone)
        {
            Graph graph = (Graph)clone;

            if (graph.Vertices.Count == 2)
            {
                return graph.Edges.Count;
            }

            Edge removedEdge = RemoveRandomEdge(graph);
            ClearEdges(graph, removedEdge);
            return RemoveVertex(graph);
        }

        private void ClearEdges(Graph graph, Edge removedEdge)
        {
            graph.Vertices.Remove(removedEdge.EndPoint);

            foreach (var edge in graph.Edges)
            {
                if (edge.EndPoint == removedEdge.EndPoint)
                {
                    edge.EndPoint = removedEdge.StartPoint;
                }

                if (edge.StartPoint == removedEdge.EndPoint)
                {
                    edge.StartPoint = removedEdge.StartPoint;
                }
            }

            var edgesToRemove = graph.Edges.Where(edge => edge.StartPoint == edge.EndPoint).ToList();

            foreach (var edge in edgesToRemove)
            {
                graph.Edges.Remove(edge);
            }
        }

        private Edge RemoveRandomEdge(Graph graph)
        {
            var rnd = new Random();

            var edge = graph.Edges[rnd.Next(graph.Edges.Count)];
            graph.Edges.Remove(edge);
            return edge;
        }
    }

    class Program
    {
        static int Main()
        {
            KargerMinCut kargerMinCut = new KargerMinCut();
            int minCut = 1000;

            for (int i = 0; i < 1000; i++)
            {
                Graph graph = CreateGraph("InputFile.txt");
                int tempMinCut = kargerMinCut.MinCut(graph);
                if (tempMinCut < minCut)
                {
                    minCut = tempMinCut;
                }
            }

            Console.WriteLine(minCut);
            return (int)ExitCode.Success;
        }

        static Graph CreateGraph(string filePath)
        {
            Graph graph = new Graph();

            using (TextReader reader = File.OpenText(@filePath))
            {
                string text = reader.ReadLine();
                while (text != null)
                {
                    string[] splited = text.Split('\t');

                    int vertex;
                    if (int.TryParse(splited[0], out vertex))
                    {
                        graph.Vertices.Add(vertex);

                        for (var i = 1; i < splited.Length; i++)
                        {
                            int endpoint;
                            if (int.TryParse(splited[i], out endpoint))
                            {
                                graph.Edges.Add(new Edge
                                {
                                    StartPoint = vertex,
                                    EndPoint = endpoint
                                });
                            }
                        }
                    }

                    text = reader.ReadLine();
                }
            }

            return graph;
        }
    }
}