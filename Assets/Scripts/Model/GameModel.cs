using System.Data;
using Tools;
using UnityEngine;

namespace Model
{
    public abstract class GameModel : IUpdatable
    {
        protected readonly GameModelSettings Settings;

        public ReactiveProperty<Vector3> ReactivePosition { get; } = new();

        public Vector3 Position
        {
            get => ReactivePosition.Get();
            set => ReactivePosition.Set(value);
        }

        public ReactiveProperty<Quaternion> ReactiveRotation { get; } = new(Quaternion.identity);

        public Quaternion Rotation
        {
            get => ReactiveRotation.Get();
            set => ReactiveRotation.Set(value);
        }

        public Vector3 ForwardDirection => Rotation * Vector3.forward;

        protected GameModel(Vector3 position, GameModelSettings settings)
        {
            Position = position;
            Settings = settings;
        }

        public void Update()
        {
            Rotate();
            Move();
        }

        protected abstract void Rotate();

        protected abstract void Move();
    }
}