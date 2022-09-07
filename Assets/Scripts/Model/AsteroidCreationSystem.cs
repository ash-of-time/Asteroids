using UnityEngine;

namespace Model
{
    public class AsteroidCreationSystem : EnemyCreationSystem<Asteroid>
    {
        public AsteroidCreationSystem(EnemySettings enemySettings, Field field, Player player) : base(enemySettings, field, player)
        {
        }

        protected override Asteroid CreateEnemyObject(Vector3 position, EnemySettings asteroidSettings)
        {
            return new Asteroid(position, asteroidSettings);
        }
    }
}