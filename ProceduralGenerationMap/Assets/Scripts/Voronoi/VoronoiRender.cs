using System.Collections.Generic;
using Geometry;
using UnityEngine;
using Utils;

namespace Voronoi
{
    public class VoronoiRender : MonoBehaviour
    {
        public static VoronoiRender Instance { get; private set; }
        
        [Header("Voronoi Rendering")]
        public Color delaunayTriangleColor = Color.cyan;
        public bool renderDelaunayTriangle = true;
        public Color siteColor = Color.red;
        public bool renderSiteColor = true;
        public Color cellColor = Color.yellow;
        public bool renderCell = true;
        public Color superTriangleColor = Color.green;
        public bool renderSupertriangle = true;
        public Color smallestCircleColor = Color.blue;
        public bool renderSmallestCircle = true;
        
        private VoronoiGenerator _generator;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            _generator = VoronoiGenerator.Instance;
        }
        
        private void OnDrawGizmos()
        {
            if (_generator == null) 
                return;
            
            if (_generator.diagram == null || _generator.diagram.cells == null)
                return;

            if (renderSupertriangle)
            {
                Gizmos.color = superTriangleColor;
                Gizmos.DrawLine(_generator.superTriangle.v0, _generator.superTriangle.v1);
                Gizmos.DrawLine(_generator.superTriangle.v0, _generator.superTriangle.v2);
                Gizmos.DrawLine(_generator.superTriangle.v1, _generator.superTriangle.v2);
            }

            if (renderSmallestCircle)
            {
                Gizmos.color = smallestCircleColor;
                UtilsClass.DrawCircle(_generator.smallestCircle.Center, _generator.smallestCircle.Radius, 64);
            }

            if (renderDelaunayTriangle)
            {
                Gizmos.color = delaunayTriangleColor;
                if (_generator.triangles != null)
                {
                    foreach (Triangle t in _generator.triangles)
                    {
                        Gizmos.DrawLine(t.v0, t.v1);
                        Gizmos.DrawLine(t.v1, t.v2);
                        Gizmos.DrawLine(t.v2, t.v0);
                    }
                }
            }

            if (renderCell)
            {
                Gizmos.color = siteColor;
                foreach (VoronoiCell cell in _generator.diagram.cells)
                {

                    Gizmos.DrawSphere(new Vector3(cell.site.x, cell.site.y, 0f), 0.05f);
                    Gizmos.color = cellColor;
                    if (cell.vertices == null || cell.vertices.Count == 0)
                        continue;
                    
                    for (int i = 0; i < cell.vertices.Count; i++)
                    {
                        Vector2 a = cell.vertices[i];
                        Vector2 b = cell.vertices[(i + 1) % cell.vertices.Count];   
                        Gizmos.DrawLine(a, b);
                    }
                }
            }

        }
        
    }
}