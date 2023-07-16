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
        private readonly ReactiveProperty<float> _velocity = new();
        private readonly ReactiveProperty<int> _laserCharges = new();
        private readonly ReactiveProperty<float> _laserReloadTime = new();

        public float GivenRotation { get; set; }
        public float GivenAcceleration { get; set; }
        public Vector3 BarrelPosition => Position.Value + Rotation.Value * PlayerSettings.BarrelPosition;

        public IReadOnlyReactiveProperty<float> Velocity => _velocity;

        public IReadOnlyReactiveProperty<int> LaserCharges => _laserCharges;

        public IReadOnlyReactiveProperty<float> LaserReloadTime => _laserReloadTime;

        private PlayerSettings PlayerSettings => Settings as PlayerSettings;

        public event Action Fired;

        public Player(Vector3 position, Quaternion rotation, PlayerSettings playerSettings, IField field) : base(position, rotation, playerSettings, field)
        {
            _laserCharges.Value = PlayerSettings.LaserCharges;
            _laserReloadTime.Value = playerSettings.LaserReloadTime;
        }
        
        public void Fire()
        {
            Fired?.Invoke();
        }

        public bool TryAlternativeFire(IEnumerable<IGameModel> hitModelsList)
        {
            if (LaserCharges.Value == 0)
                return false;

            _laserCharges.Value--;
            _laserReloadTime.Value = PlayerSettings.LaserReloadTime;
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
            _velocity.Value = velocity;
            if (_velocityDirection == -ForwardDirection && velocity > 0)
                return;

            _position.Value += _currentVelocityVector * Time.deltaTime;
            
            base.Move();
        }

        private void Rotate()
        {
            _rotation.Value *= Quaternion.Euler(Vector3.up * (GivenRotation * PlayerSettings.RotationSpeed * Time.deltaTime));
        }

        private void ReloadLaser()
        {
            if (LaserCharges.Value < PlayerSettings.LaserCharges)
            {
                var laserReloadTime = Mathf.Clamp(LaserReloadTime.Value - Time.deltaTime, 0,
                    PlayerSettings.LaserReloadTime);
                _laserReloadTime.Value = laserReloadTime;
                if (laserReloadTime == 0)
                {
                    _laserCharges.Value++;
                    _laserReloadTime.Value = PlayerSettings.LaserReloadTime;
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