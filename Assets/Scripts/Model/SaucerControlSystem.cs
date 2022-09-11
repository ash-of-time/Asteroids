using UnityEngine;

namespace Model
{
    public class SaucerControlSystem : EnemyControlSystem
    {
        public SaucerControlSystem(EnemySettings enemySettings, IField field, GameModel player) : base(enemySettings, field, player)
        {
        }

        protected override GameModel CreateGameModelObject(Vector3 position)
        {
            return new Saucer(position, EnemySettings, Player, Field);
        }
    }
}