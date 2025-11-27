using Unity.Mathematics;
using UnityEngine;
using Voronoi;

namespace Texture
{
    public class VoronoiTexture : MonoBehaviour
    {
        private Texture2D texture;
        public VoronoiDiagram voronoi; 
        public int Resolution;
        public float2 scale = 1F;

        void Start()
        {
            Texture2D texture = new Texture2D(Resolution, Resolution);

            for (int y = 0; y < Resolution; y++)
            {
                for (int x = 0; x < Resolution; x++)
                {
                    float2 uv = new float2((float)x / Resolution, (float)y / Resolution);
                    float2 pos = uv * scale;
                    
                    
                    /** Add Voronoi Region Id System */
                }
            }

            
            GetComponent<Renderer>().material.mainTexture = texture;
        }
    }
}