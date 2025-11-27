using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils
{
    // Basic utils class from a personnal project
    public static class UtilsClass
    {
        #region WorldText
        // Create Text in the world
        public static TextMesh CreateWorldText(string text, Transform parent = null,
            Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor anchor = TextAnchor.MiddleCenter, 
            TextAlignment alignment = TextAlignment.Center, int sortingOrder = 0)
        {
            if (color == null) color = Color.white;
            return CreateWorldText_Internal(parent, text, localPosition, fontSize, (Color)color, anchor, alignment, sortingOrder);
        }

        // Create Text in the world
        private static TextMesh CreateWorldText_Internal(Transform parent, string text, Vector3 localPosition, int fontSize,
            Color color, TextAnchor anchor, TextAlignment alignment, int sortingOrder)
        {
            GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.text = text;
            textMesh.anchor = anchor;
            textMesh.alignment = alignment;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
            textMesh.GetComponent<Renderer>().sortingOrder = sortingOrder;
            return textMesh;
        }
    
        #endregion
        
        #region Mouse Position

        public static Vector3 GetMouseWorldPosition(Vector2 screenPos)
        {
            Vector3 vec = GetMouseWorldPositionWithZ(screenPos, Camera.main);
            vec.z = 0;
            return vec;
        }
    
        public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }
        #endregion 
    
        #region Array Operations

        public static void Shuffle<T>(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {              
                int r = Random.Range(i, array.Length);
                (array[i], array[r]) = (array[r], array[i]);
            }
        }
    
        #endregion
    }
}
