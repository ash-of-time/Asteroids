using UnityEngine;

namespace Model
{
    public class AsteroidControlSystem : EnemyControlSystem
    {
        public AsteroidControlSystem(EnemySettings enemySettings, IField field, GameModel player) : base(enemySettings, field, player)
        {
        }

        protected override GameModel CreateGameModelObject(Vector3 position)
        {
            return new Asteroid(position, EnemySettings, Field);
        }
    }
}