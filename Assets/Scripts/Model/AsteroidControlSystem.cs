using UnityEngine;

namespace Model
{
    public class AsteroidControlSystem : EnemyControlSystem<Asteroid>
    {
        public AsteroidControlSystem(EnemySettings enemySettings, Field field, Player player) : base(enemySettings, field, player)
        {
        }

        protected override Asteroid CreateEnemyObject(Vector3 position)
        {
            return new Asteroid(position, EnemySettings, Field);
        }
    }
}