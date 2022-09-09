using UnityEngine;

namespace Model
{
    public class Player : GameModel
    {
        private Vector3 _velocityDirection = Vector3.zero;
        private Vector3 _currentVelocityVector;

        public float GivenRotation { get; set; }
        public float GivenAcceleration { get; set; }

        private PlayerSettings PlayerSettings => Settings as PlayerSettings;

        public Player(PlayerSettings playerSettings, Field field) : base(playerSettings.InitialPosition, playerSettings)
        {
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
            if (_velocityDirection == -ForwardDirection && velocity > 0)
                return;

            Position += _currentVelocityVector * Time.deltaTime;
        }

        protected override void Rotate()
        {
            Rotation *= Quaternion.Euler(Vector3.up * (GivenRotation * PlayerSettings.RotationSpeed * Time.deltaTime));
        }

        public void Fire()
        {
        }

        public void AlternativeFire()
        {
        }
    }
}