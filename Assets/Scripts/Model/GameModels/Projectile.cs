using UnityEngine;

namespace Model
{
    public class Projectile : GameModel
    {
        private readonly IMovingStrategy _movingStrategy;
        
        public Projectile(Vector3 position, Quaternion rotation, GameModelSettings settings, IField field) : base(position, rotation, settings, field)
        {
            _movingStrategy = new StraightMovingStrategy(this);
        }

        protected override void Move()
        {
            _position.Value += _movingStrategy.MoveDelta();
            if (Field.IsPointOutOfField(Position.Value))
                Destroy(false);
        }
    }
}