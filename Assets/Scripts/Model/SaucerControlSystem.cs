using UnityEngine;

namespace Model
{
    public class SaucerControlSystem : EnemyControlSystem<Saucer>
    {
        public SaucerControlSystem(EnemySettings enemySettings, Field field, Player player) : base(enemySettings, field, player)
        {
        }

        protected override Saucer CreateEnemyObject(Vector3 position)
        {
            return new Saucer(position, EnemySettings, Player, Field);
        }
    }
}