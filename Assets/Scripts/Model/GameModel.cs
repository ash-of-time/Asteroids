using Tools;
using UnityEngine;

namespace Model
{
    public abstract class GameModel
    {
        public ReactiveProperty<Vector3> ReactivePosition { get; } = new();

        public Vector3 Position
        {
            get => ReactivePosition.Get();
            set => ReactivePosition.Set(value);
        }
        
        public ReactiveProperty<Quaternion> ReactiveRotation { get; } = new();

        public Quaternion Rotation
        {
            get => ReactiveRotation.Get();
            set => ReactiveRotation.Set(value);
        }

        protected GameModel(Vector3 position)
        {
            Position = position;
        }
    }
}