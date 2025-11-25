using System;
using System.Collections.Generic;
using Geometry;
using UnityEngine;
using Utils;
using Voronoi;

namespace Testing
{
    public class DelaunayTriangulationTest : MonoBehaviour
    {
        private List<Triangle> triangles;
        private Vector2[] points;
        private Circle smallestCircle;
        private Triangle superTriangle;
        private void Start()
        {
            points = VoronoiGenerator.Instance.GenerateRandomPoints(); 
            smallestCircle = WelzAlgorithm.WelzlInitialization(points);
            superTriangle = VoronoiGenerator.Instance.MakeSuperTriangle(smallestCircle);
            triangles = DelaunayTriangulation.BowyerWatson(points, superTriangle);
        }
        
        private void OnDrawGizmos()
        {
            if (points == null || points.Length == 0) return;
            
            Gizmos.color = Color.green;
            foreach (var p in points)
                Gizmos.DrawSphere(p, 0.05f);
            
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(smallestCircle.Center, 0.2f);
            
            Gizmos.color = Color.blue;
            UtilsClass.DrawCircle(smallestCircle.Center, smallestCircle.Radius, 64);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(superTriangle.v0, superTriangle.v1);
            Gizmos.DrawLine(superTriangle.v0, superTriangle.v2);
            Gizmos.DrawLine(superTriangle.v1, superTriangle.v2);
            
            Gizmos.color = Color.cyan;
            if (triangles != null)
            {
                foreach (Triangle t in triangles)
                {
                    Gizmos.DrawLine(t.v0, t.v1);
                    Gizmos.DrawLine(t.v1, t.v2);
                    Gizmos.DrawLine(t.v2, t.v0);
                }
            }
        }
    }
}