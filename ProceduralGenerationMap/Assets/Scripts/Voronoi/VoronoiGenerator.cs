using System;
using System.Collections.Generic;
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
        
        public VoronoiDiagram diagram { get; private set; }
        public List<Triangle> triangles { get; private set; }
        public Triangle superTriangle { get; private set; }
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
            smallestCircle = WelzAlgorithm.WelzlInitialization(points);
            superTriangle = WelzAlgorithm.MakeSuperTriangle(smallestCircle);
            triangles = DelaunayTriangulation.BowyerWatson(points, superTriangle);
            diagram = new VoronoiDiagram(triangles, points);
        }
    }
}
