using System;
using Tools;
using UnityEngine;

namespace Model
{
    public class Player : GameModel, IPlayer
    {
        private Vector3 _velocityDirection = Vector3.zero;
        private Vector3 _currentVelocityVector;

        public float GivenRotation { get; set; }
        public float GivenAcceleration { get; set; }
        public Vector3 BarrelPosition => Position + Rotation * PlayerSettings.BarrelPosition;

        public ReactiveProperty<float> ReactiveVelocity = new();

        private float Velocity
        {
            get => ReactiveVelocity.Get();
            set => ReactiveVelocity.Set(value);
        }

        private PlayerSettings PlayerSettings => Settings as PlayerSettings;

        public event Action Fired;

        public Player(Vector3 position, Quaternion rotation, PlayerSettings playerSettings, IField field) : base(position, rotation, playerSettings, field)
        {
        }
        
        public void Fire()
        {
            Fired?.Invoke();
        }

        public void AlternativeFire()
        {
        }

        public override void Update()
        {
            Rotate();
            base.Update();
        }

        protected override void Move()
        {
            var accelerationDirection = ForwardDirection;
            var currentAcceleration = GivenAcceleration * PlayerSettings.Acceleration * accelerationDirection;

            var decelerationDirection = -_velocityDirection;
            var currentDeceleration = PlayerSettings.Deceleration * decelerationDirection;

            _currentVelocityVector = Vector3.ClampMagnitude(_currentVelocityVector + (currentAcceleration + currentDeceleration) * Time.deltaTime,
                PlayerSettings.MaxVelocity);

            _velocityDirection = _currentVelocityVector.normalized;

            Velocity = _currentVelocityVector.magnitude;
            if (_velocityDirection == -ForwardDirection && Velocity > 0)
                return;

            Position += _currentVelocityVector * Time.deltaTime;
            
            base.Move();
        }

        private void Rotate()
        {
            Rotation *= Quaternion.Euler(Vector3.up * (GivenRotation * PlayerSettings.RotationSpeed * Time.deltaTime));
        }
        
        // todo remove Temp
        public override void Collide(GameModel gameModel)
        {
            // do nothing
        }
    }
}