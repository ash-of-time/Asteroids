using UnityEngine;

namespace Model
{
    public interface IField
    {
        public Vector3 GetRandomPosition();

        public Vector3 GetRandomPositionFarFromPoint(Vector3 point, float minDistance);

        public Vector3 GetPointFromOtherSideIfOutOfField(Vector3 point);
    }
}