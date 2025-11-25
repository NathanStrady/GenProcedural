using UnityEngine;
using Voronoi;

namespace Testing
{
    public class TriangleTest : MonoBehaviour
    {
        [SerializeField] private bool runTest = true;
        [SerializeField] private Triangle triangle = new Triangle(
            new Vector2(0, 0),
            new Vector2(4, 0),
            new Vector2(2, 3)
        );
        
        [Header("Test Point (D)")]
        [SerializeField] Vector2 D = new Vector2(2, 1);
        private void Start()
        {
            if (!runTest) return;

            Debug.Log("Triangle (from Inspector):");
            Debug.Log($"A = {triangle.v0}, B = {triangle.v1}, C = {triangle.v2}");

            Vector2 center = triangle.CircumCircle.Center;
            Debug.Log($"Circumcenter = {center}");

            bool inside = triangle.CircumCircle.Contains(D);
            Debug.Log($"Point D {D} inside circumcircle ? {inside}");

            Vector2 farPoint = new Vector2(50, 50); 
            Debug.Log($"Point {farPoint} inside circumcircle ? {triangle.CircumCircle.Contains(farPoint)}");
        }
        
        private void OnDrawGizmos()
        {
            if (!runTest) return;
            
            Vector2 p0 = triangle.v0;
            Vector2 p1 = triangle.v1;
            Vector2 p2 = triangle.v2;

            Triangle tri = new Triangle(p0, p1, p2);

            Circle circle = tri.CircumCircle;
            float radius = Vector2.Distance(circle.Center, p0);

            Vector3 u0 = new Vector3(p0.x, p0.y, 0);
            Vector3 u1 = new Vector3(p1.x, p1.y, 0);
            Vector3 u2 = new Vector3(p2.x, p2.y, 0);
            Vector3 c = new Vector3(circle.Center.x, circle.Center.y, 0);
            
            Gizmos.color = Color.white;
            Gizmos.DrawLine(u0, u1);
            Gizmos.DrawLine(u1, u2);
            Gizmos.DrawLine(u2, u0);
            
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(c, 0.1f);
            
            Gizmos.color = Color.green;
            int steps = 80;
            for (int i = 0; i < steps; i++)
            {
                float a1 = (i / (float)steps) * Mathf.PI * 2f;
                float a2 = ((i + 1) / (float)steps) * Mathf.PI * 2f;

                Vector3 pA = c + new Vector3(Mathf.Cos(a1) * radius, Mathf.Sin(a1) * radius, 0);
                Vector3 pB = c + new Vector3(Mathf.Cos(a2) * radius, Mathf.Sin(a2) * radius, 0);

                Gizmos.DrawLine(pA, pB);
            }
            
            Vector3 d = new Vector3(D.x, D.y, 0);

            bool inside = tri.CircumCircle.Contains(D);

            Gizmos.color = inside ? Color.green : Color.red;
            Gizmos.DrawSphere(d, 0.1f);

            Gizmos.DrawLine(c, d);
        }
    }
}