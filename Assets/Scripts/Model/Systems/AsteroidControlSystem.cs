using UnityEngine;

namespace Model
{
    public class AsteroidControlSystem : EnemyControlSystem
    {
        public AsteroidControlSystem(EnemySettings enemySettings, GameModelControlSystem relatedControlSystem) : base(enemySettings, relatedControlSystem)
        {
        }

        protected override GameModel CreateGameModelObject(Vector3 position, Quaternion rotation)
        {
            var customRotation = Quaternion.Euler(0, Random.Range(-180f, 180f), 0);
            return new Asteroid(position, customRotation, EnemySettings, Field.Instance);
        }
    }
}