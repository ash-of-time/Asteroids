using UnityEngine;

namespace Model
{
    public class Field
    {
        private readonly Vector2 _size;

        public float HalfWidth => _size.x / 2;
        public float HalfHeight => _size.y / 2;

        public Field(FieldSettings fieldSettings)
        {
            _size = fieldSettings.Size;
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
    }
}