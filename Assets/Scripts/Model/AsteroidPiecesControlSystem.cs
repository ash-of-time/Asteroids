using UnityEngine;

namespace Model
{
    public class AsteroidPiecesControlSystem : AsteroidControlSystem
    {
        public EnemyControlSystem AsteroidControlSystem { get; set; }

        public AsteroidPiecesControlSystem(EnemyControlSystem asteroidControlSystem, EnemySettings asteroidPiecesSettings, IField field) : base(asteroidPiecesSettings, field, null)
        {
            AsteroidControlSystem = asteroidControlSystem;
        }

        public override void Initialize()
        {
            AsteroidControlSystem.GameModelDestroyed += OnAsteroidDestroyed;
        }

        private void OnAsteroidDestroyed(GameModel asteroid)
        {
            var count = Random.Range(EnemySettings.InitialCount, EnemySettings.MaxCount + 1);
            for (var i = 0; i < count; i++)
            {
                CreateGameModel(asteroid.Position);
            }
        }
    }
}