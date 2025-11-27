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

            for (int i = 0; i < sites.Length; i++)
            {
                Vector2 site = sites[i];
                Region currRegion = new Region(site);

                foreach (var tri in delauney.triangleByID)
                {
                    if (tri.Value.HasVertex(site))
                    {
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