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
        public Color cellColor = Color.yellow;
        public bool renderCell = true;
        public Color superTriangleColor = Color.green;
        public bool renderSupertriangle = true;
        public Color smallestCircleColor = Color.blue;
        public bool renderSmallestCircle = true;
        public Color circumCircleColor = Color.darkBlue;
        public bool renderCircle = true;
        
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
                _generator.superTriangle.DrawGizmos(superTriangleColor);
            }

            if (renderSmallestCircle)
            {
                _generator.smallestCircle.DrawGizmos(smallestCircleColor);
            }

            if (renderDelaunayTriangle)
            {
                if (_generator.triangles != null)
                {
                    foreach (Triangle t in _generator.triangles)
                    {
                        t.DrawGizmos(delaunayTriangleColor);
                    }
                }
            }
            
            if (renderCircle)
            {
                foreach (Triangle t in _generator.triangles)
                {
                    t.CircumCircle.DrawGizmos(circumCircleColor);
                }
            }

            if (renderCell)
            {
                foreach (VoronoiCell cell in _generator.diagram.cells)
                {
                    cell.DrawVoronoiCell(cellColor);
                }
            }

        }
        
    }
}