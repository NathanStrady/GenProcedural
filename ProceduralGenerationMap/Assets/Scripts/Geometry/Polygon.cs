using System;
using System.Collections.Generic;
using UnityEngine;

namespace Geometry
{
    public struct Polygon : IEquatable<Polygon>
    {
        public List<Vector2> Vertices;

        public Polygon(List<Vector2> vertices)
        {
            Vertices = vertices;
        }
        
        public int VertexCount => Vertices?.Count ?? 0;

        public Edge GetEdge(int index)
        {
            if (VertexCount < 2)
                return new Edge();

            Vector2 a = Vertices[index];
            Vector2 b = Vertices[(index + 1) % VertexCount]; 

            return new Edge(a, b);
        }
        
        public List<Edge> GetEdges()
        {
            List<Edge> edges = new List<Edge>();
            if (VertexCount < 2)
                return edges;

            for (int i = 0; i < VertexCount; i++)
                edges.Add(GetEdge(i));

            return edges;
        }
        
        public void DrawGizmos(Color color)
        {
            if (VertexCount < 2)
                return;

            Gizmos.color = color;

            for (int i = 0; i < VertexCount; i++)
            {
                Vector2 a = Vertices[i];
                Vector2 b = Vertices[(i + 1) % VertexCount]; // wrap
                Gizmos.DrawLine(a, b);
            }
        }
        
        public override bool Equals(object obj)
        {
            if (!(obj is Polygon other))
                return false;

            if (other.VertexCount != VertexCount)
                return false;

            for (int i = 0; i < VertexCount; i++)
            {
                if (!Vertices[i].Equals(other.Vertices[i]))
                    return false;
            }

            return true;
        }

        public bool Equals(Polygon other)
        {
            return Equals(Vertices, other.Vertices);
        }

        public override int GetHashCode()
        {
            return (Vertices != null ? Vertices.GetHashCode() : 0);
        }
    }
}