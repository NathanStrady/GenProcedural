using System.Collections.Generic;
using UnityEngine;

namespace Geometry
{
    public class Region
    {
        public Vector2 Site;
        public List<Vector2> Vertices;

        public Region(Vector2 site)
        {
            Site = site;
            Vertices = new List<Vector2>();
        }

        public void AddVertexCcw(Vector2 newPoint)
        {
            if (Vertices.Count == 0)
            {
                Vertices.Add(newPoint);
                return;
            }
            
            float newAngle = Mathf.Atan2(newPoint.y - Site.y, newPoint.x - Site.x);
            
            for (int i = 0; i < Vertices.Count; i++)
            {
                Vector2 v = Vertices[i];
                float angle = Mathf.Atan2(v.y - Site.y, v.x - Site.x);

                if (newAngle < angle)
                {
                    Vertices.Insert(i, newPoint);
                    return;
                }
            }

            Vertices.Add(newPoint);
        }

        public void DrawGizmo(Color color)
        {
            if (Vertices.Count < 2) return; 

            Gizmos.color = color;
            Gizmos.DrawSphere(Site, 0.1f);
            for (int i = 0; i < Vertices.Count; i++)
            {
                Vector2 current = Vertices[i];
                Vector2 next = Vertices[(i + 1) % Vertices.Count];
                Gizmos.DrawLine(current, next);
                Gizmos.DrawSphere(current, 0.1f);
            }
        }
    }
}