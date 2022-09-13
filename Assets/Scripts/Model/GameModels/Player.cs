using System;
using System.Collections.Generic;
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

        public ReactiveProperty<float> ReactiveVelocity { get; } = new();

        public float Velocity
        {
            get => ReactiveVelocity.Get();
            private set => ReactiveVelocity.Set(value);
        }
        
        public ReactiveProperty<int> ReactiveLaserCharges { get; } = new();

        public int LaserCharges
        {
            get => ReactiveLaserCharges.Get();
            private set => ReactiveLaserCharges.Set(value);
        }
        
        public ReactiveProperty<float> ReactiveLaserReloadTime { get; } = new();

        public float LaserReloadTime
        {
            get => ReactiveLaserReloadTime.Get();
            private set => ReactiveLaserReloadTime.Set(value);
        }

        private PlayerSettings PlayerSettings => Settings as PlayerSettings;

        public event Action Fired;

        public Player(Vector3 position, Quaternion rotation, PlayerSettings playerSettings, IField field) : base(position, rotation, playerSettings, field)
        {
            LaserCharges = PlayerSettings.LaserCharges;
            LaserReloadTime = playerSettings.LaserReloadTime;
        }
        
        public void Fire()
        {
            Fired?.Invoke();
        }

        public bool TryAlternativeFire(List<GameModel> hitModelsList)
        {
            if (LaserCharges == 0)
                return false;

            LaserCharges--;
            LaserReloadTime = PlayerSettings.LaserReloadTime;
            foreach (var gameModel in hitModelsList)
            {
                gameModel.Destroy(true);
            }

            return true;
        }

        public override void Update()
        {
            ReloadLaser();
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

        private void ReloadLaser()
        {
            if (LaserCharges < PlayerSettings.LaserCharges)
            {
                LaserReloadTime = Mathf.Clamp(LaserReloadTime - Time.deltaTime, 0, PlayerSettings.LaserReloadTime);
                if (LaserReloadTime == 0)
                {
                    LaserCharges++;
                    LaserReloadTime = PlayerSettings.LaserReloadTime;
                }
            }
        }
        
        public override void Collide(GameModel gameModel)
        {
            if (!PlayerSettings.IsInvulnerable)
                base.Collide(gameModel);
        }
    }
}