using System;
using Geometry;
using UnityEngine;
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
        
        public Triangle MakeSuperTriangle(Circle mec)
        {
            Vector2 center = mec.Center;
            float radius = mec.Radius * 2;
            
            Vector2 v1 = center + new Vector2(0, radius);
            Vector2 v2 = center + new Vector2(-radius * Mathf.Sin(Mathf.PI / 3f), -radius * Mathf.Cos(Mathf.PI / 3f));
            Vector2 v3 = center + new Vector2(radius * Mathf.Sin(Mathf.PI / 3f), -radius * Mathf.Cos(Mathf.PI / 3f));
            
            return new Triangle(v1, v2, v3);
        }
    }
}
