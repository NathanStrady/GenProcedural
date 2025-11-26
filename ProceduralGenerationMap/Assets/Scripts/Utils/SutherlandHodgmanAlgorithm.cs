using System.Collections.Generic;
using Geometry;
using UnityEngine;

namespace Utils
{
    public static class SutherlandHodgmanAlgorithm
    {
        public static Polygon Clip(Polygon subjectPolygon, Polygon clipPolygon)
        {
            List<Vector2> outputList = subjectPolygon.Vertices;

            foreach (Edge clipEdge in clipPolygon.GetEdges())
            {
                List<Vector2> inputList = outputList;
                outputList.Clear();
                
                if (inputList.Count == 0)
                    break;
                
                for (int i = 0; i < inputList.Count; i++)
                {
                    Vector2 currPoint = inputList[i];
                    Vector2 prevPoint = inputList[(i - 1) % inputList.Count];
                    LinearEquation line1 = new LinearEquation(prevPoint, currPoint);
                    LinearEquation line2 = new LinearEquation(clipEdge.v0, clipEdge.v1);
                    
                    line1.TryIntersection(line2, out Vector2 intersectionPoint);

                    if (Inside(currPoint, clipEdge.v0, clipEdge.v1))
                    {
                        if (!Inside(prevPoint, clipEdge.v0, clipEdge.v1))
                        {
                            outputList.Add(intersectionPoint);
                        }
                        outputList.Add(currPoint);
                    } else if (Inside(prevPoint, clipEdge.v0, clipEdge.v1))
                    {
                        outputList.Add(intersectionPoint);
                    }
                }
            }
            
            return new Polygon(outputList);
        }
        
        private static bool Inside(Vector2 p, Vector2 edgeStart, Vector2 edgeEnd)
        {
            return (edgeEnd.x - edgeStart.x) * (p.y - edgeStart.y) -
                (edgeEnd.y - edgeStart.y) * (p.x - edgeStart.x) >= 0;
        }
    }
}