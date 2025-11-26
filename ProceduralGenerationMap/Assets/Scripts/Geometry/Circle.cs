

using UnityEngine;

namespace Geometry
{
    [System.Serializable]
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

        public bool Contains(Vector2[] p)
        {
            bool isValid = true;
            int i = 0;
            while (isValid && i < p.Length)
            {
                isValid = Contains(p[i]);
                i++;
            }

            return isValid;
        }
    }
}