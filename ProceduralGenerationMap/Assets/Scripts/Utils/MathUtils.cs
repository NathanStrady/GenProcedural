using System.Collections.Generic;
using Geometry;
using UnityEngine;
using Voronoi;

namespace Utils
{
    public static class MathUtils 
    {
        #region MEC (Minium Enclonsing Circle)
        public static Circle TwoPointMinimumEnclosingCircle(Vector2 pointA, Vector2 pointB)
        {
            Vector2 center = (pointA + pointB) / 2;
            float radius = Vector2.Distance(center, pointA);
            return new Circle(center, radius);
        }

        public static Circle ThreePointMinimumEnclosingCircle(Vector2 pointA, Vector2 pointB, Vector2 pointC)
        {
            
            LinearEquation lineAB = new LinearEquation(pointA, pointB);
            LinearEquation lineBC = new LinearEquation(pointB, pointC);

            Vector2 midPointAB = Vector2.Lerp(pointA, pointB, 0.5f);
            Vector2 midPointBC = Vector2.Lerp(pointB, pointC, 0.5f);

            LinearEquation perpendicularAB = lineAB.PerpendicularLineAt(midPointAB);
            LinearEquation perpendicularBC = lineBC.PerpendicularLineAt(midPointBC);

            Vector2 circumCircle = GetCrossingPoint(perpendicularAB, perpendicularBC);
            
            return new Circle(circumCircle, Vector2.Distance(circumCircle, pointA));
        }
        
        private static Vector2 GetCrossingPoint(LinearEquation line1, LinearEquation line2)
        {
            float A1 = line1.A;
            float A2 = line2.A;
            float B1 = line1.B;
            float B2 = line2.B;
            float C1 = line1.C;
            float C2 = line2.C;

            float determinant = A1 * B2 - A2 * B1;
            float determinantX = C1 * B2 - C2 * B1;
            float determinantY = A1 * C2 - A2 * C1;

            float x = determinantX / determinant;
            float y = determinantY / determinant;

            return new Vector2(x, y);
        }
        
        #endregion
        
        #region 
        
        #endregion 
    }
}