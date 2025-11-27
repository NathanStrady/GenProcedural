using System;
using System.Collections.Generic;
using Geometry;
using UnityEngine;
using Voronoi;

namespace Utils
{
    /**
     * Algorithm that determine the MEC (Minimum Enclosing Circle) around a group of point
     * Useful link : https://en.wikipedia.org/wiki/Smallest-circle_problem
     */
    public static class WelzAlgorithm
    {
        private static Circle Trivial(Vector2[] r, int nr)
        {
            // Return an empty circle
            if (nr == 0)
            {
                return new Circle(Vector2.zero, 0f);
            }

            // Return an empty circle with the current border point at his center
            if (nr == 1)
            {
                return new Circle(r[0], 0f);
            }

            // We make a circle with the two border points
            if (nr == 2)
            {
                return MathUtils.TwoPointMinimumEnclosingCircle(r[0], r[1]);
            }

            // If we have three points, maybe the pair of three point can make a circle that is good for our cases
            if (nr == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = i + 1; j < 3; j++)
                    {
                        Circle c = MathUtils.TwoPointMinimumEnclosingCircle(r[i], r[j]);
                        if (c.Contains(r))
                        {
                            return c;
                        }
                    }
                }
            }

            // We create the circle with our three points. Three points that form a triangle and so we can get his circumcircle as our minimal enclosing circle
            return MathUtils.ThreePointMinimumEnclosingCircle(r[0], r[1], r[2]);
        }
        
        private static Circle Welzl(Vector2[] P, Vector2[] R, int n, int nr)
        {
            // If we have no point to calculate or we have found the three points on the border. We can handling the trivials cases of our minimum circle
            if (n == 0 || nr == 3)
                return Trivial(R, nr);

            // We're getting a random point in our list.
            int index = UnityEngine.Random.Range(0, n);
            
            // Application of the P - {p}, we delete our current selected point of our points list
            // Optimization, we do not delete the value but we keep it at the end. We will never going back at it in the algorithm
            Vector2 p = P[index];
            P[index] = P[n - 1];
            P[n - 1] = p;
            
            // Get the minimal circle with P - {p} (Points without the previously selected point)
            Circle D = Welzl(P, R, n - 1, nr);
            
            // If it contains p, we have our minimal circle
            if (D.Contains(p))
                return D;

            // Else our current point is part of the border of the minimal circle
            R[nr] = p;

            // Recall the algorithm with the point that is in the border
            return Welzl(P,  R, n - 1, nr + 1);
        }
        
        public static Circle WelzlInitialization(Vector2[] points)
        {
            UtilsClass.Shuffle(points); // We shuffle our points for the algorithm 
            Vector2[] R = new Vector2[3];
            return Welzl(points, R, points.Length, 0);
        }
        
        public static DelaunayTriangle MakeSuperTriangle(Circle mec)
        {
            Vector2 center = mec.Center;
            float radius = mec.Radius * 2; // In order to get all the point with the triangle, we rescale the circle.
            
            Vector2 v1 = center + new Vector2(0, radius);
            Vector2 v2 = center + new Vector2(-radius * Mathf.Sin(Mathf.PI / 3f), -radius * Mathf.Cos(Mathf.PI / 3f));
            Vector2 v3 = center + new Vector2(radius * Mathf.Sin(Mathf.PI / 3f), -radius * Mathf.Cos(Mathf.PI / 3f));
            
            return new DelaunayTriangle(v1, v2, v3);
        }
    }
}
