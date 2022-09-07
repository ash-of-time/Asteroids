using UnityEngine;

namespace Model
{
    public class SaucerCreationSystem : EnemyCreationSystem<Saucer>
    {
        public SaucerCreationSystem(EnemySettings enemySettings, Field field, Player player) : base(enemySettings, field, player)
        {
        }

        protected override Saucer CreateEnemyObject(Vector3 position, EnemySettings asteroidSettings)
        {
            return new Saucer(position, asteroidSettings);
        }
    }
}