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
        public Vector3 BarrelPosition => Position.Value + Rotation.Value * PlayerSettings.BarrelPosition;

        public ReactiveProperty<float> Velocity { get; } = new();
        
        public ReactiveProperty<int> LaserCharges { get; } = new();
        
        public ReactiveProperty<float> LaserReloadTime { get; } = new();

        private PlayerSettings PlayerSettings => Settings as PlayerSettings;

        public event Action Fired;

        public Player(Vector3 position, Quaternion rotation, PlayerSettings playerSettings, IField field) : base(position, rotation, playerSettings, field)
        {
            LaserCharges.Value = PlayerSettings.LaserCharges;
            LaserReloadTime.Value = playerSettings.LaserReloadTime;
        }
        
        public void Fire()
        {
            Fired?.Invoke();
        }

        public bool TryAlternativeFire(IEnumerable<IGameModel> hitModelsList)
        {
            if (LaserCharges.Value == 0)
                return false;

            LaserCharges.Value--;
            LaserReloadTime.Value = PlayerSettings.LaserReloadTime;
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

            var velocity = _currentVelocityVector.magnitude;
            Velocity.Value = velocity;
            if (_velocityDirection == -ForwardDirection && velocity > 0)
                return;

            Position.Value += _currentVelocityVector * Time.deltaTime;
            
            base.Move();
        }

        private void Rotate()
        {
            Rotation.Value *= Quaternion.Euler(Vector3.up * (GivenRotation * PlayerSettings.RotationSpeed * Time.deltaTime));
        }

        private void ReloadLaser()
        {
            if (LaserCharges.Value < PlayerSettings.LaserCharges)
            {
                var laserReloadTime = Mathf.Clamp(LaserReloadTime.Value - Time.deltaTime, 0,
                    PlayerSettings.LaserReloadTime);
                LaserReloadTime.Value = laserReloadTime;
                if (laserReloadTime == 0)
                {
                    LaserCharges.Value++;
                    LaserReloadTime.Value = PlayerSettings.LaserReloadTime;
                }
            }
        }
        
        public override void Collide(IGameModel gameModel)
        {
            if (!PlayerSettings.IsInvulnerable)
                base.Collide(gameModel);
        }
    }
}