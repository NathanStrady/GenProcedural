using System;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using Utils;
using Voronoi;

namespace Geometry
{
    [Serializable]
    // Delaunay structure that contains each vertex of the triangle and also each neighbor of the triangle.
    public class DelaunayTriangle
    {
        public int index; 
        public Vector2 v0, v1, v2;
        public DelaunayTriangle a0, a1, a2;
        public DelaunayTriangle(Vector2 v0, Vector2 v1, Vector2 v2)
        {
            this.index = -1;
            this.v0 = v0;
            this.v1 = v1;
            this.v2 = v2;
            this.a0 = this.a1 = this.a2 = null;
        }

        public DelaunayTriangle(int index, Vector2 v0, Vector2 v1, Vector2 v2)
        {
            this.index = index;
            this.v0 = v0;
            this.v1 = v1;
            this.v2 = v2;
            this.a0 = this.a1 = this.a2 = null;
        }
        
        public Circle CircumCircle => MathUtils.ThreePointMinimumEnclosingCircle(v0, v1, v2);
        
        public void GetEdges(out Edge e0, out Edge e1, out Edge e2)
        {
            e0 = new Edge(v0, v1);
            e1 = new Edge(v0, v2);
            e2 = new Edge(v1, v2);
        }
        
        public List<DelaunayTriangle> GetAdjacent()
        {
            List<DelaunayTriangle> adjacentTriangles = new List<DelaunayTriangle>();
            
            if (a0 != null) adjacentTriangles.Add(a0);
            if (a1 != null) adjacentTriangles.Add(a1);
            if (a2 != null) adjacentTriangles.Add(a2);
            
            return adjacentTriangles;
        }
        
        public bool SetAdjacent(DelaunayTriangle neighbor)
        {
            if (neighbor == null || Equals(neighbor, this))
                return false;
            
            GetEdges(out Edge e0, out Edge e1, out Edge e2);
            neighbor.GetEdges(out Edge n0, out Edge n1, out Edge n2);
            
            if (e0.Equals(n0) || e0.Equals(n1) || e0.Equals(n2))
            {
                a0 = neighbor;
                return true;
            }
            if (e1.Equals(n0) || e1.Equals(n1) || e1.Equals(n2))
            {
                a1 = neighbor;
                return true;
            }
            if (e2.Equals(n0) || e2.Equals(n1) || e2.Equals(n2))
            {
                a2 = neighbor;
                return true;
            }

            return false;
        }

        
        public bool SharesEdgeWith(Edge e, DelaunayTriangle other)
        {
            if (other.Equals(this))
                return false;
            
            other.GetEdges(out Edge otherE0, out Edge otherE1, out Edge otherE2);

            return e.Equals(otherE0) || e.Equals(otherE1) || e.Equals(otherE2);

        }
        
        public bool SharesEdgeWith(DelaunayTriangle other)
        {
            if (other.Equals(this))
                return false;
            
            GetEdges(out Edge e0, out Edge e1, out Edge e2);
            other.GetEdges(out Edge o0, out Edge o1, out Edge o2);

            return e0.Equals(o0) || e0.Equals(o1) || e0.Equals(o2) ||
                   e1.Equals(o0) || e1.Equals(o1) || e1.Equals(o2) ||
                   e2.Equals(o0) || e2.Equals(o1) || e2.Equals(o2);
        }
        
        public bool IsAdjacentTo(DelaunayTriangle other)
        {
            if (Equals(other)) return false;
            return SharesEdgeWith(other);
        }
        
        public bool HasVertex(Vector2 p)
        {
            return p.Equals(v0) || p.Equals(v1) || p.Equals(v2);
        }
        
        public void DrawGizmos(Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(v0, v1);
            Gizmos.DrawSphere(v0, 0.05f);
            Gizmos.DrawLine(v1, v2);
            Gizmos.DrawSphere(v1, 0.05f);
            Gizmos.DrawLine(v2, v0);
            Gizmos.DrawSphere(v2, 0.05f);
        }
        
        public void DrawNeighbor(Color neighborColor)
        {
            List<DelaunayTriangle> neighbors = GetAdjacent();
            if (neighbors.Count == 0)
                return;
            
            Gizmos.color = neighborColor;
            foreach (DelaunayTriangle neighbor in neighbors)
            {
                neighbor.DrawGizmos(neighborColor);
            }
        }
    }
}