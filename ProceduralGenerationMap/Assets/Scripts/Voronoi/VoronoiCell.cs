using System.Collections.Generic;
using Geometry;
using UnityEngine;

namespace Voronoi
{
    public class VoronoiCell
    {
        public Vector2 Site;
        public Polygon Polygon;
        
        public VoronoiCell(Vector2 site, Polygon polygon)
        {
            Site = site;
            Polygon = polygon;
        }

        public void DrawVoronoiCell(Color color)
        {
            Gizmos.DrawSphere(Site, 0.05f);
            Polygon.DrawGizmos(color);
        }
    }
}