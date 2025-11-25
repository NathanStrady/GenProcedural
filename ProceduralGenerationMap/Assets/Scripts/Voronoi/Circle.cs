

using UnityEngine;

namespace Voronoi
{
    public struct Circle
    {
        public Vector2 Center;
        public readonly float Radius;

        public Circle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public bool Contains(Vector2 p)
        {
            return Vector2.Distance(Center, p) <= Radius;
        }
    }
}