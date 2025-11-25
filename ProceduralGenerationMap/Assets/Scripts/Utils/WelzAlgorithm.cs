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
            if (nr == 0)
            {
                return new Circle(Vector2.zero, 0f);
            }

            if (nr == 1)
            {
                return new Circle(r[0], 0f);
            }

            if (nr == 2)
            {
                return MathUtils.TwoPointMinimumEnclosingCircle(r[0], r[1]);
            }

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

            return MathUtils.ThreePointMinimumEnclosingCircle(r[0], r[1], r[2]);
        }
        
        private static Circle Welzl(Vector2[] P, Vector2[] R, int n, int nr)
        {
            if (n == 0 || nr == 3)
                return Trivial(R, nr);

            int index = UnityEngine.Random.Range(0, n);
            
            // Optimization, we do not delete the value but we keep it at the end. We will never going back at it in the algorithm
            Vector2 p = P[index];
            P[index] = P[n - 1];
            P[n - 1] = p;
            
            Circle D = Welzl(P, R, n - 1, nr);
            if (D.Contains(p))
                return D;

            R[nr] = p;

            return Welzl(P,  R, n - 1, nr + 1);
        }

        public static Circle WelzlInitialization(Vector2[] points)
        {
            UtilsClass.Shuffle(points);
            Vector2[] R = new Vector2[3];
            return Welzl(points, R, points.Length, 0);
        }
    }
}
