using UnityEngine;

namespace Model
{
    public class SaucerControlSystem : EnemyControlSystem
    {
        public SaucerControlSystem(EnemySettings enemySettings, IField field, PlayerControlSystem playerControlSystem) : base(enemySettings, field, playerControlSystem)
        {
        }

        protected override GameModel CreateGameModelObject(Vector3 position, Quaternion rotation)
        {
            return new Saucer(position, rotation, EnemySettings, Player, Field);
        }
    }
}