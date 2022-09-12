using System;
using Tools;
using UnityEngine;

namespace Model
{
    public abstract class GameModel : IUpdatable
    {
        protected readonly GameModelSettings Settings;
        private readonly IField _field;

        public ReactiveProperty<Vector3> ReactivePosition { get; } = new();

        public Vector3 Position
        {
            get => ReactivePosition.Get();
            protected set => ReactivePosition.Set(value);
        }

        public ReactiveProperty<Quaternion> ReactiveRotation { get; } = new(Quaternion.identity);

        public Quaternion Rotation
        {
            get => ReactiveRotation.Get();
            protected set => ReactiveRotation.Set(value);
        }

        public Vector3 ForwardDirection => Rotation * Vector3.forward;

        public event Action<GameModel> Destroyed;

        protected GameModel(Vector3 position, GameModelSettings settings, IField field)
        {
            Position = position;
            Settings = settings;
            _field = field;
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
            Position = _field.GetPointFromOtherSideIfOutOfField(Position);
        }
    }
}