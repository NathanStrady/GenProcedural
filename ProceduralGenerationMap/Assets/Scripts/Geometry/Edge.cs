using System;
using UnityEngine;

namespace Geometry
{
    [System.Serializable]
    // Edge structure that compute an edge of a polygon
    public struct Edge : IEquatable<Edge>
    {
        public Vector2 v0, v1;

        public Edge(Vector2 v0, Vector2 v1)
        {
            this.v0 = v0;
            this.v1 = v1;
        }
        public override bool Equals(object obj)
        {
            if (obj is not Edge edge)
                return false;
            
            return (v0.Equals(edge.v0) && v1.Equals(edge.v1)) ||
                   (v0.Equals(edge.v1) && v1.Equals(edge.v0));
        }
        
        public void DrawGizmos(Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(v0, v1);
        }

        public bool Equals(Edge other)
        {
            return v0.Equals(other.v0) && v1.Equals(other.v1);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(v0, v1);
        }
    }
}