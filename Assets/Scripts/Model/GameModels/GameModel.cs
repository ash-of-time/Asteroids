using System;
using Tools;
using UnityEngine;

namespace Model
{
    public abstract class GameModel : IUpdatable
    {
        protected readonly GameModelSettings Settings;
        protected readonly IField Field;

        public ReactiveProperty<Vector3> ReactivePosition { get; } = new();

        public Vector3 Position
        {
            get => ReactivePosition.Get();
            protected set => ReactivePosition.Set(value);
        }

        public ReactiveProperty<Quaternion> ReactiveRotation { get; } = new();

        public Quaternion Rotation
        {
            get => ReactiveRotation.Get();
            protected set => ReactiveRotation.Set(value);
        }

        public Vector3 ForwardDirection => Rotation * Vector3.forward;

        public event Action<GameModel> Destroyed;

        protected GameModel(Vector3 position, Quaternion rotation, GameModelSettings settings, IField field)
        {
            Position = position;
            Rotation = rotation;
            Settings = settings;
            Field = field;
        }

        public virtual void Update()
        {
            Move();
        }

        public virtual void Collide(GameModel gameModel)
        {
            Destroy();
        }
        
        public void Destroy()
        {
            Destroyed?.Invoke(this);
        }

        protected virtual void Move()
        {
            if (Field.IsPointOutOfField(Position))
                Position = Field.GetPointFromOtherSideIfOutOfField(Position);
        }
    }
}