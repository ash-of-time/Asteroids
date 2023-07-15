using UnityEngine;

namespace Model
{
    public class AsteroidControlSystem : EnemyControlSystem
    {
        public AsteroidControlSystem(Game game, EnemySettings enemySettings,
            GameModelControlSystem relatedControlSystem) : base(game, enemySettings, relatedControlSystem)
        {
        }

        protected override IGameModel CreateGameModelObject(Vector3 position, Quaternion rotation)
        {
            var customRotation = Quaternion.Euler(0, Random.Range(-180f, 180f), 0);
            return new Asteroid(position, customRotation, EnemySettings, Game.Field);
        }
    }
}