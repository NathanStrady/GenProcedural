using System.Collections.Generic;
using Geometry;
using UnityEngine;
using Utils;

namespace Voronoi
{
    public class VoronoiDiagram
    {
        public List<VoronoiCell> cells = new();
        private Polygon _bounds;

        public VoronoiDiagram(List<Triangle> delaunayTriangles, Vector2[] sites)
        {
            BuildDiagram(delaunayTriangles, sites);
            _bounds = new Polygon(new List<Vector2>
            {
                new Vector2(-10, -10),
                new Vector2(10, -10),  
                new Vector2(10, 10), 
                new Vector2(-10, 10)   
            });
            

        }

        private void BuildDiagram(List<Triangle> delaunayTriangles, Vector2[] sites)
        {
            foreach (Vector2 p in sites)
            {
                List<Triangle> adjacentTriangles = new();
                foreach (Triangle tri in delaunayTriangles)
                {
                    if (tri.HasVertex(p))
                        adjacentTriangles.Add(tri);
                }

                List<Vector2> circumCenters = new();
                foreach (Triangle tri in adjacentTriangles)
                    circumCenters.Add(tri.CircumCircle.Center);
                
                circumCenters.Sort((a, b) =>
                {
                    float angleA = Mathf.Atan2(a.y - p.y, a.x - p.x);
                    float angleB = Mathf.Atan2(b.y - p.y, b.x - p.x);
                    return angleA.CompareTo(angleB);
                });
                
                Polygon polygon = new Polygon(circumCenters);
                VoronoiCell cell = new VoronoiCell(p, polygon);
                cells.Add(cell);
            }
        }
    }
}