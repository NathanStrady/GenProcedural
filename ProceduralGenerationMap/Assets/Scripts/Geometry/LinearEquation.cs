using UnityEngine;

namespace Geometry
{
    [System.Serializable]
    public struct LinearEquation
    {
        public float A;
        public float B;
        public float C;
        
        public LinearEquation(Vector2 pointA, Vector2 pointB)
        {
            float deltax = pointB.x - pointA.x;
            float deltay = pointB.y - pointA.y;
            A = deltay;
            B = -deltax;
            C = A * pointA.x + B * pointA.y;
        }

        public LinearEquation PerpendicularLineAt(Vector2 point)
        {
            LinearEquation newLine = new LinearEquation();

            newLine.A = -B;
            newLine.B = A;
            newLine.C = newLine.A * point.x + newLine.B * point.y;

            return newLine;
        }
        
        public bool TryIntersection(LinearEquation other, out Vector2 intersection)
        {
            float det = A * other.B - other.A * B;

            if (Mathf.Approximately(det, 0f))
            {
                intersection = Vector2.zero;
                return false;
            }

            float x = (other.B * C - B * other.C) / det;
            float y = (A * other.C - other.A * C) / det;

            intersection = new Vector2(x, y);
            return true;
        }
    }
}