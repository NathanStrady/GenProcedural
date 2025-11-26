using System.Collections.Generic;
using Geometry;
using UnityEngine;

namespace Voronoi
{
    public class VoronoiCell
    {
        public Vector2 site;             
        public List<Vector2> vertices;

        public VoronoiCell(Vector2 site)
        {
            this.site = site;
            vertices = new();
        }
    }
}