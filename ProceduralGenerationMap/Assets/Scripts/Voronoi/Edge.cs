using System.Numerics;

namespace Voronoi
{
    public struct Edge
    {
        private Vector2 v0, v1;

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
    }
}