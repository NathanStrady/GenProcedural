using System.Collections.Generic;
using Geometry;
using UnityEngine;
using Utils;

namespace Voronoi
{
    public class VoronoiDiagram
    {
        public List<VoronoiCell> cells = new();
        public Rect bounds;

        public VoronoiDiagram(List<Triangle> delaunayTriangles, Vector2[] sites)
        {
            BuildDiagram(delaunayTriangles, sites);    
        }

        private void BuildDiagram(List<Triangle> delaunayTriangles, Vector2[] sites)
        {
            foreach (Vector2 p in sites)
            {
                VoronoiCell cell = new VoronoiCell(p);
                List<Triangle> adjacentTriangle = new();
                foreach (Triangle tri in delaunayTriangles)
                {
                    if (tri.HasVertex(p))
                    {
                        adjacentTriangle.Add(tri);
                    }
                }
                
                List<Vector2> circumCenters = new();
                foreach (Triangle tri in adjacentTriangle)
                {
                    circumCenters.Add(tri.CircumCircle.Center);
                }
                
                circumCenters.Sort((a, b) =>
                {
                    float angleA = Mathf.Atan2(a.y - p.y, a.x - p.x);
                    float angleB = Mathf.Atan2(b.y - p.y, b.x - p.x);
                    return angleA.CompareTo(angleB);
                });
                
                cell.vertices.AddRange(circumCenters);
                cells.Add(cell);
            }
        }
    }
}