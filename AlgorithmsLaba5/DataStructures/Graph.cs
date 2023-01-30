using AlgorithmsLaba5.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlgorithmsLaba5.DataStructures
{
    internal class Graph
    {
        public List<Vertex> Vertices { get; set; }
        public List<Edge> Edges { get; private set; }
        public int countVertices { get; set; }
        public int countEdges { get; set; }
        public Graph()
        {
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();
        }
        public void AddVertex(Vertex vertex)
        {
            countVertices++;
            vertex.Id = countVertices;
            Vertices.Add(vertex);
        }
        public void AddEdge(Edge edge)
        {
            countEdges++;
            edge.Id = countEdges;
            Edges.Add(edge);
            if (!Vertices.Contains(edge.From))
            {
                Vertices.Add(edge.From);
            }
            if (!Vertices.Contains(edge.To))
            {
                Vertices.Add(edge.To);
            }
        }
        public void AddEdge(Vertex from, Vertex to)
        {
            var edge = new Edge(from, to);
            Edges.Add(edge);
            if (!Vertices.Contains(edge.From))
            {
                Vertices.Add(edge.From);
            }
            if (!Vertices.Contains(edge.To))
            {
                Vertices.Add(edge.To);
            }
        }
        public void RemoveVertex(Vertex vertex)
        {
            List<Edge> deleteVertex = new List<Edge>();
            foreach (var edge in Edges)
            {
                if (edge.From.Equals(vertex) || edge.To.Equals(vertex))
                {
                    deleteVertex.Add(edge);
                }
            }
            foreach (var item in deleteVertex)
            {
                Edges.Remove(item);
            }
            Vertices.Remove(vertex);
        }
        public void RemoveEdge(Edge edge)
        {
            Edges.Remove(edge);
            edge = null;
        }
        public List<Vertex> GetVertexList(Vertex vertex)
        {
            var result = new List<Vertex>();
            foreach (var edge in Edges)
            {
                if (edge.From.Equals(vertex))
                {
                    result.Add(edge.To);
                }
                else if (edge.To.Equals(vertex))
                {
                    result.Add(edge.From);
                }
            }
            return result;
        }
        public void BleachingVertices()
        {
            foreach (var item in Vertices)
            {
                item.Backlight = false;
            }
        }
        public void BleachingEdges()
        {
            foreach (var item in Edges)
            {
                item.Backlight = false;
            }
        }
        public List<Edge> GetOreintedFromEdgeList(Vertex vertex)
        {
            var result = new List<Edge>();
            foreach (var edge in Edges)
            {
                if (edge.From.Equals(vertex))
                {
                    result.Add(edge);
                }
                else if (edge.To.Equals(vertex))
                {
                    if (!edge.Oreinted)
                    {
                        result.Add(edge);
                    }
                }
            }
            return result;
        }
        public List<Edge> GetOreintedToEdgeList(Vertex vertex)
        {
            var result = new List<Edge>();
            foreach (var edge in Edges)
            {
                if (edge.To.Equals(vertex))
                {
                    result.Add(edge);
                }
                else if (edge.From.Equals(vertex))
                {
                    if (!edge.Oreinted)
                    {
                        result.Add(edge);
                    }
                }
            }
            return result;
        }
        public List<Edge> GetEdgeList(Vertex vertex)
        {
            var result = new List<Edge>();
            foreach (var edge in Edges)
            {
                if (edge.From.Equals(vertex))
                {
                    result.Add(edge);
                }
                else if (edge.To.Equals(vertex))
                {
                    result.Add(edge);
                }
            }
            return result;
        }
        public Edge GetEdgeTwoVertex(Vertex vertexA, Vertex vertexB)
        {
            foreach (var edge in Edges)
            {
                if (edge.From.Equals(vertexA) && edge.To.Equals(vertexB))
                {
                    return edge;
                }
                else if (edge.To.Equals(vertexA) && edge.From.Equals(vertexB))
                {
                    return edge;
                }
            }
            throw new ArgumentException("Ребро не найдено(((");
        }
        public int[,] GetMatrix()
        {
            var matrix = new int[Vertices.Count, Vertices.Count];
            foreach (var edge in Edges)
            {
                var row = edge.From.Id - 1;
                var column = edge.To.Id - 1;

                matrix[row, column] = edge.Weight;
            }
            return matrix;
        }
    }
}
