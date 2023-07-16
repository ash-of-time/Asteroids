using UnityEngine;

namespace Model
{
    public class Saucer : Enemy
    {
        private readonly GameModel _player;

        public Saucer(Vector3 position, Quaternion rotation, EnemySettings saucerSettings, GameModel player, IField field) : base(position, rotation, saucerSettings, field)
        {
            _player = player;
            MovingStrategy = new FollowMovingStrategy(this, player);
        }
    }
}