using UnityEngine;

namespace Geometry
{
    [System.Serializable]
    public struct BoundingBox
    {
        public float xmin;
        public float ymin;
        public float xmax;
        public float ymax;
        
        // Corners;
        public Vector2 BottomLeft  => new Vector2(xmin, ymin);
        public Vector2 BottomRight => new Vector2(xmax, ymin);
        public Vector2 TopLeft     => new Vector2(xmin, ymax);
        public Vector2 TopRight    => new Vector2(xmax, ymax);
        
        public BoundingBox(float xmin, float ymin, float xmax, float ymax)
        {
            this.xmin = xmin;
            this.ymin = ymin;
            this.xmax = xmax;
            this.ymax = ymax;
        }

        public BoundingBox(Vector2 min, Vector2 max)
        {
            xmin = min.x;
            ymin = min.y;
            xmax = max.x;
            ymax = max.y;
        }
        
        public bool Contains(Vector2 p)
        {
            return p.x >= xmin && p.x <= xmax &&
                   p.y >= ymin && p.y <= ymax;
        }
        
        public void DrawGizmos(Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(BottomLeft,  BottomRight);
            Gizmos.DrawLine(BottomRight, TopRight);
            Gizmos.DrawLine(TopRight,    TopLeft);
            Gizmos.DrawLine(TopLeft,     BottomLeft);
        }

    }
}