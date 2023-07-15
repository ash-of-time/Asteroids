using UnityEngine;

namespace Model
{
    public class SaucerControlSystem : EnemyControlSystem
    {
        public SaucerControlSystem(Game game, EnemySettings enemySettings, PlayerControlSystem playerControlSystem) :
            base(game, enemySettings, playerControlSystem)
        {
        }

        protected override IGameModel CreateGameModelObject(Vector3 position, Quaternion rotation)
        {
            return new Saucer(position, rotation, EnemySettings, Player, Game.Field);
        }
    }
}