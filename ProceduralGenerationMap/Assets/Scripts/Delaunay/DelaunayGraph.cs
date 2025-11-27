using System.Collections.Generic;
using Geometry;
using UnityEngine;

namespace Delaunay
{
    public class DelaunayGraph
    {
        public Dictionary<int, DelaunayTriangle> triangleByID;
        public Dictionary<int, List<int>> adjacencyList;

        public DelaunayGraph(Vector2[] points, DelaunayTriangle superDelaunayTriangle)
        {
            List<DelaunayTriangle> triangles = BowyerWatson(points, superDelaunayTriangle);
            BuildAdjacencyList(triangles);
        }

        // https://en.wikipedia.org/wiki/Bowyer%E2%80%93Watson_algorithm//
        private List<DelaunayTriangle> BowyerWatson(Vector2[] points, DelaunayTriangle superDelaunayTriangle)
        {
            int ID = 0;
            superDelaunayTriangle.index = ID++;
            
            List<DelaunayTriangle> triangulation = new List<DelaunayTriangle>();
            triangulation.Add(superDelaunayTriangle);
            
            foreach (Vector2 point in points)
            {
                List<DelaunayTriangle> badTriangle = new List<DelaunayTriangle>();
                // We determine each triangle are "bad" ones.
                foreach (DelaunayTriangle triangle in triangulation)
                {
                    // If the current point is inside the circumcircle of the triangle, it is a bad one
                    if (triangle.CircumCircle.Contains(point))
                    {
                        badTriangle.Add(triangle);
                    }
                }

                List<Edge> polygon = new List<Edge>();
                // We loop in each bad triangle in order to get their share edges with the current one. 
                for (int i = 0; i < badTriangle.Count; i++)
                {
                    DelaunayTriangle curr = badTriangle[i];
                    curr.GetEdges(out Edge e0, out Edge e1, out Edge e2);

                    bool shared0 = false;
                    bool shared1 = false;
                    bool shared2 = false;

                    for (int j = 0; j < badTriangle.Count; j++)
                    {
                        if (i == j) continue;

                        DelaunayTriangle next = badTriangle[j];

                        if (curr.SharesEdgeWith(e0, next)) shared0 = true;
                        if (curr.SharesEdgeWith(e1, next)) shared1 = true;
                        if (curr.SharesEdgeWith(e2, next)) shared2 = true;
                    }

                    if (!shared0) polygon.Add(e0);
                    if (!shared1) polygon.Add(e1);
                    if (!shared2) polygon.Add(e2);
                }

                // We loop into each bad triangle to delete them of the final triangulation
                foreach (DelaunayTriangle triangle in badTriangle)
                {
                    triangulation.Remove(triangle);
                }

                // We construct new triangle with the edge of bad ones and the current point that serve to detect if the triangle that we loop in is the bad one.
                foreach (Edge edge in polygon)
                {
                    DelaunayTriangle newTri = new DelaunayTriangle(edge.v0, edge.v1, point);
                    triangulation.Add(newTri);
                }
            }

            // We delete the all triangle that have a common vertex with the super one for clean up
            triangulation.RemoveAll(triangle =>
                triangle.HasVertex(superDelaunayTriangle.v0) ||
                triangle.HasVertex(superDelaunayTriangle.v1) ||
                triangle.HasVertex(superDelaunayTriangle.v2));

            // Assign an ID to the rest of the triangle
            foreach (var triangle in triangulation)
            {
                triangle.index = ID;
                ID++;
            }
            
            LinkNeighbors(triangulation);
            return triangulation;
        }

        private void LinkNeighbors(List<DelaunayTriangle> triangles)
        {
            for (int i = 0; i < triangles.Count; i++)
            {
                DelaunayTriangle t1 = triangles[i];
                for (int j = i + 1; j < triangles.Count; j++)
                {
                    DelaunayTriangle t2 = triangles[j];
                    
                    // If they share an edge so they are neighbor
                    if (t1.SharesEdgeWith(t2))
                    {
                        t1.SetAdjacent(t2);
                        t2.SetAdjacent(t1);
                    }
                }
            }
        }

        private void BuildAdjacencyList(List<DelaunayTriangle> triangles)
        {
            triangleByID = new Dictionary<int, DelaunayTriangle>();
            adjacencyList = new Dictionary<int, List<int>>();

            foreach (DelaunayTriangle tri in triangles)
            {
                triangleByID[tri.index] = tri;
                adjacencyList[tri.index] = new List<int>();
            }

            foreach (var currTriangle in triangles)
            {
                List<DelaunayTriangle> currNeighbors = currTriangle.GetAdjacent();
                foreach (DelaunayTriangle neighbor in currNeighbors)
                {
                    if (!adjacencyList[currTriangle.index].Contains(neighbor.index))
                    {
                        adjacencyList[currTriangle.index].Add(neighbor.index);
                    }
                }
            }
        }

        public void DrawGizmo(Color color)
        {
            Gizmos.color = color;
            foreach (var kvp in triangleByID)
            {
                DelaunayTriangle tri = kvp.Value;
                tri.DrawGizmos(color);
            }
        }
    }
}