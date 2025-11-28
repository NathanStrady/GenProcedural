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
        public Color pointColor = Color.darkGoldenRod;
        public bool renderPoint = true; 
        
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

            if (renderPoint)
            {
                Gizmos.color = pointColor;
                foreach (Vector2 p in _generator.points)
                {
                    Gizmos.DrawSphere(p, 0.05f);
                }
            }


            if (renderSupertriangle)
            {
                _generator.SuperDelaunayTriangle.DrawGizmos(superTriangleColor);
            }

            if (renderSmallestCircle)
            {
                _generator.smallestCircle.DrawGizmos(smallestCircleColor);
            }

            if (renderDelaunayTriangle)
            {
                _generator.DelaunayGraph.DrawGizmo(delaunayTriangleColor);
            }
            
            if (renderCell)
            {
                _generator.diagram.DrawGizmo(cellColor);
            }

            if (renderCircle)
            {
                foreach (var tri in _generator.DelaunayGraph.triangleByID)
                {
                    tri.Value.CircumCircle.DrawGizmos(circumCircleColor);
                }

            }

            

        }
        
    }
}