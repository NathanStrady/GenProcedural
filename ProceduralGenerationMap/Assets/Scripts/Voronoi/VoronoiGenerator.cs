using System;
using System.Collections.Generic;
using Delaunay;
using Geometry;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Voronoi
{
    public class VoronoiGenerator : MonoBehaviour
    {
        public static VoronoiGenerator Instance { get; private set; }
        
        [Header("Voronoï Parameters")]
        [SerializeField] private int maxNumberOfPoints = 1000;
        [SerializeField] private Vector2 areaMin = new Vector2(-10, -10);
        [SerializeField] private Vector2 areaMax = new Vector2(10, 10);

        public DelaunayGraph DelaunayGraph;
        public VoronoiDiagram diagram { get; private set; }
        public List<DelaunayTriangle> triangles { get; private set; }
        public DelaunayTriangle SuperDelaunayTriangle { get; private set; }
        public Circle smallestCircle { get; private set; }
        
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

        private void Start()
        {
            BuildVoronoi();
        }

        public Vector2[] GenerateRandomPoints()
        {
            Vector2[] points = new Vector2[maxNumberOfPoints];
        
            for (int i = 0; i < maxNumberOfPoints; i++)
            {
                float x = Random.Range(areaMin.x, areaMax.x);
                float y = Random.Range(areaMin.y, areaMax.y);
                points[i] = new Vector2(x, y);
            }

            return points;
        }
        
        public void BuildVoronoi()
        {
            Vector2[] points = GenerateRandomPoints();
            
            // Find the MEC (Minimul Enclosing Circle)
            smallestCircle = WelzAlgorithm.WelzlInitialization(points); 
            
            // Make a super triangle with the MEC
            SuperDelaunayTriangle = WelzAlgorithm.MakeSuperTriangle(smallestCircle);
            
            // Build the delaunay graph following the bowser-watson algorithm
            DelaunayGraph = new DelaunayGraph(points, SuperDelaunayTriangle);
            
            // Build the voronoi diagram
            diagram = new VoronoiDiagram(DelaunayGraph, points);
        }
    }
}
