using System;
using Tools;
using UnityEngine;

namespace Model
{
    public abstract class GameModel : IGameModel
    {
        protected readonly GameModelSettings Settings;
        protected readonly IField Field;

        public ReactiveProperty<Vector3> Position { get; } = new();

        public ReactiveProperty<Quaternion> Rotation { get; } = new();

        public Vector3 ForwardDirection => Rotation.Value * Vector3.forward;

        public event Action<IGameModel, bool> Destroyed;

        protected GameModel(Vector3 position, Quaternion rotation, GameModelSettings settings, IField field)
        {
            Position.Value = position;
            Rotation.Value = rotation;
            Settings = settings;
            Field = field;
        }

        public virtual void Update()
        {
            Move();
        }

        public virtual void Collide(IGameModel gameModel)
        {
            Destroy(false);
        }
        
        public void Destroy(bool totally)
        {
            Destroyed?.Invoke(this, totally);
        }

        protected virtual void Move()
        {
            var position = Position.Value;
            if (Field.IsPointOutOfField(position))
                Position.Value = Field.GetPointFromOtherSideIfOutOfField(position);
        }
    }
}