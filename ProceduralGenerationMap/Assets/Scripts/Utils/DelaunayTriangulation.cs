using System.Collections.Generic;
using Geometry;
using UnityEditor.ShaderGraph.Legacy;
using UnityEngine;
using Voronoi;

namespace Utils
{
    // Not optimize but we do not care with the limited time I have
    public static class DelaunayTriangulation
    {
        public static List<Triangle> BowyerWatson(Vector2[] points, Triangle superTriangle)
        {
            List<Triangle> triangulation = new List<Triangle>();
            triangulation.Add(superTriangle);
            
            foreach (Vector2 point in points)
            {
                List<Triangle> badTriangle = new List<Triangle>();
                foreach (Triangle triangle in triangulation)
                {
                    if (triangle.CircumCircle.Contains(point))
                    {
                        badTriangle.Add(triangle);
                    }
                }

                List<Edge> polygon = new List<Edge>();
                foreach (Triangle triangle in badTriangle)
                {
                    triangle.GetEdges(out Edge e0, out Edge e1, out Edge e2);
                    if (!triangle.HasSharedEdgeWith(e0, badTriangle)) 
                        polygon.Add(e0);
                    if (!triangle.HasSharedEdgeWith(e1, badTriangle)) 
                        polygon.Add(e1);
                    if (!triangle.HasSharedEdgeWith(e2, badTriangle)) 
                        polygon.Add(e2);
                }

                foreach (Triangle triangle in badTriangle)
                {
                    triangulation.Remove(triangle);
                }

                foreach (Edge edge in polygon)
                {
                    Triangle newTri = new Triangle(edge.v0, edge.v1, point);
                    triangulation.Add(newTri);
                }
            }
            
            triangulation.RemoveAll(triangle =>
                triangle.HasVertex(superTriangle.v0) ||
                triangle.HasVertex(superTriangle.v1) ||
                triangle.HasVertex(superTriangle.v2));
            
            return triangulation;
        }
    }
}