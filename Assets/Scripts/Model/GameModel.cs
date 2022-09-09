using Tools;
using UnityEngine;

namespace Model
{
    public abstract class GameModel : IUpdatable
    {
        protected readonly GameModelSettings Settings;
        protected readonly Field Field;

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

        protected GameModel(Vector3 position, GameModelSettings settings, Field field)
        {
            Position = position;
            Settings = settings;
            Field = field;
        }

        public virtual void Update()
        {
            Move();
        }

        protected virtual void Move()
        {
            Position = Field.GetPointFromOtherSideIfOutOfField(Position);
        }
    }
}