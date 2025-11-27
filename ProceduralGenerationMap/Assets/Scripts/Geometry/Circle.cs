

using UnityEngine;

namespace Geometry
{
    [System.Serializable]
    // Basic Circle structure that contains basic functions (Contains, DrawGizmos)
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
        
        public void DrawGizmos(Color color, int segments = 64)
        {
            Gizmos.color = color;

            float angleStep = 360f / segments;
            Vector3 prevPoint = Center + new Vector2(Radius, 0);

            for (int i = 1; i <= segments; i++)
            {
                float angle = angleStep * i * Mathf.Deg2Rad;
                Vector3 nextPoint = Center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Radius;

                Gizmos.DrawLine(prevPoint, nextPoint);
                prevPoint = nextPoint;
            }
        }
    }
}