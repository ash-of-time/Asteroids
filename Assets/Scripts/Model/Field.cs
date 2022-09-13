using UnityEngine;

namespace Model
{
    public class Field : IField
    {
        public static IField Instance;

        private readonly Vector2 _size;

        public float HalfWidth => _size.x / 2;
        public float HalfHeight => _size.y / 2;
        public float Diagonal => Mathf.Sqrt(Mathf.Pow(_size.x, 2) + Mathf.Pow(_size.y, 2));

        private Field(FieldSettings fieldSettings)
        {
            _size = fieldSettings.Size;
        }

        public static void CreateInstance(FieldSettings fieldSettings)
        {
            if (Instance == null)
                Instance = new Field(fieldSettings);
        }

        public Vector3 GetRandomPosition()
        {
            var x = Random.Range(-HalfWidth, HalfWidth);
            var z = Random.Range(-HalfHeight, HalfHeight);

            return new Vector3(x, 0, z);
        }

        public Vector3 GetRandomPositionFarFromPoint(Vector3 point, float minDistance)
        {
            Vector3 position;
            do
            {
                position = GetRandomPosition();
            }
            while (Vector3.Distance(position, point) < minDistance);

            return position;
        }

        public bool IsPointOutOfField(Vector3 point)
        {
            if (point.x < -HalfWidth || point.x > HalfWidth || point.z < -HalfHeight || point.z > HalfHeight)
                return true;

            return false;
        }
        
        public Vector3 GetPointFromOtherSideIfOutOfField(Vector3 point)
        {
            var x = point.x;
            var z = point.z;
            
            if (x < -HalfWidth)
                x = HalfWidth;
            else if (x > HalfWidth)
                x = -HalfWidth;
            
            if (z < -HalfHeight)
                z = HalfHeight;
            else if (z > HalfHeight)
                z = -HalfHeight;

            return new Vector3(x, 0, z);
        }
    }
}