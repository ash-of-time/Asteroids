using UnityEngine;

namespace Model
{
    public interface IField
    {
        public float Diagonal { get; }
        
        public Vector3 GetRandomPosition();

        public Vector3 GetRandomPositionFarFromPoint(Vector3 point, float minDistance);

        public bool IsPointOutOfField(Vector3 point);

        public Vector3 GetPointFromOtherSideIfOutOfField(Vector3 point);
    }
}