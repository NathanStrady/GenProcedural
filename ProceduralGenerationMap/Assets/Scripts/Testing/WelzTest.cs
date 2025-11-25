using UnityEngine;
using Utils;
using Voronoi;

namespace Testing
{
    public class WelzTest : MonoBehaviour
    {
        [SerializeField] private bool runTest = false;
        
        private VoronoiGenerator _generator;
        
        
        private Vector2[] points;
        private Circle smallestCircle;
        
        
        void Start()
        {
            if (!runTest) return;
            _generator = new VoronoiGenerator();
            points = _generator.GenerateRandomPoints(); 
            smallestCircle = WelzAlgorithm.WelzlInitialization(points);
        }
        
        private void OnDrawGizmos()
        {
            if (!runTest) return;
            if (_generator == null) return;
            
            Gizmos.color = Color.green;
            foreach (var p in points)
                Gizmos.DrawSphere(p, 0.05f);
            
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(smallestCircle.Center, 0.2f);
            
            Gizmos.color = Color.blue;
            UtilsClass.DrawCircle(smallestCircle.Center, smallestCircle.Radius, 64);
        }
        
        
    }
}