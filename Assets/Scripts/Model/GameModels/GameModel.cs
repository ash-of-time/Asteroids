using System;
using Tools;
using UnityEngine;

namespace Model
{
    public abstract class GameModel : IGameModel
    {
        protected readonly IField Field;
        protected ReactiveProperty<Vector3> _position = new();
        protected ReactiveProperty<Quaternion> _rotation = new();
        
        public GameModelSettings Settings  { get; private set; }

        public IReadOnlyReactiveProperty<Vector3> Position => _position;

        public IReadOnlyReactiveProperty<Quaternion> Rotation => _rotation;

        public Vector3 ForwardDirection => Rotation.Value * Vector3.forward;

        public event Action<IGameModel, bool> Destroyed;

        protected GameModel(Vector3 position, Quaternion rotation, GameModelSettings settings, IField field)
        {
            _position.Value = position;
            _rotation.Value = rotation;
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
                _position.Value = Field.GetPointFromOtherSideIfOutOfField(position);
        }
    }
}