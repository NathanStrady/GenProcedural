using UnityEngine;

namespace Voronoi
{
    public class VoronoiGenerator
    {
        private int maxNumberOfPoints = 1000;
        private Vector2 areaMin = new Vector2(-10, -10);
        private Vector2 areaMax = new Vector2(10, 10);
        
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
    }
}
