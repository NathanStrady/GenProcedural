using Unity.Mathematics.Geometry;
using UnityEngine;
using Utils;

namespace Voronoi
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

        public Circle CircumCircle => MathUtils.ThreePointMinimumEnclosingCircle(v0, v1, v2);

        public override bool Equals(object obj)
        {
            if (obj is not Triangle t)
                return false;
            
            return
                (v0 == t.v0 || v0 == t.v1 || v0 == t.v2) &&
                (v1 == t.v0 || v1 == t.v1 || v1 == t.v2) &&
                (v2 == t.v0 || v2 == t.v1 || v2 == t.v2);
        }
    }
}