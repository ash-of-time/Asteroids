using UnityEngine;

namespace Model
{
    public class Saucer : Enemy
    {
        private readonly GameModel _player;

        public Saucer(Vector3 position, Quaternion rotation, EnemySettings saucerSettings, GameModel player, IField field) : base(position, rotation, saucerSettings, field)
        {
            _player = player;
        }

        protected override void Move()
        {
            var direction = (_player.Position - Position).normalized;
            Position += direction * (Settings.MaxVelocity * Time.deltaTime);
            
            base.Move();
        }
    }
}