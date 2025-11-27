using System.Collections.Generic;
using Delaunay;
using Geometry;
using UnityEngine;
using Utils;

namespace Voronoi
{
    public class VoronoiDiagram
    {
        public List<Region> Regions;
        public VoronoiDiagram(DelaunayGraph delauney, Vector2[] sites)
        {
            BuildDiagram(delauney, sites);
        }
        
        private void BuildDiagram(DelaunayGraph delauney, Vector2[] sites)
        {
            int ID = 0;
            Regions = new List<Region>();
            
            // Build the diagram by iterating through each point of the delauney graph
            for (int i = 0; i < sites.Length; i++)
            {
                Vector2 site = sites[i];
                Region currRegion = new Region(site);
                
                // A site = a vertex of a triangle and so we loop through each delauney triangle in order to find each triangles that contain the current site 
                foreach (var tri in delauney.triangleByID)
                {
                    if (tri.Value.HasVertex(site))
                    {
                        // When we have it, we add to the current region and counter clockwise the circumcenter as a vertex of the region 
                        // Each circumcenter of a triangle, by the duality of both graph, is a vertex of a region in the voronoi diagram
                        currRegion.AddVertexCcw(tri.Value.CircumCircle.Center);
                    }
                }
                
                Regions.Add(currRegion);
            }
        }

        public void DrawGizmo(Color color)
        {
            foreach (Region region in Regions)
            {
                region.DrawGizmo(color);
            }
        }
    }
}