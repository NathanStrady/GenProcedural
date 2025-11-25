using System.Collections.Generic;
using UnityEngine;
using Utils;
using Voronoi;

namespace Geometry
{
    [System.Serializable]
    public struct Triangle
    {
        public Vector2 v0, v1, v2;
        public Triangle(Vector2 v0, Vector2 v1, Vector2 v2)
        {
            this.v0 = v0;
            this.v1 = v1;
            this.v2 = v2;
        }
        
        public void GetEdges(out Edge e0, out Edge e1, out Edge e2)
        {
            e0 = new Edge(v0, v1);
            e1 = new Edge(v0, v2);
            e2 = new Edge(v1, v2);
        }
        
        public bool HasEdge(Edge e)
        {
            Edge e0, e1, e2;
            GetEdges(out e0, out e1, out e2);
            return e.Equals(e0) || e.Equals(e1) || e.Equals(e2);
        }

        public bool HasVertex(Vector2 p)
        {
            return p.Equals(v0) || p.Equals(v1) || p.Equals(v2);
        }

        public Circle CircumCircle => MathUtils.ThreePointMinimumEnclosingCircle(v0, v1, v2);
    }
}