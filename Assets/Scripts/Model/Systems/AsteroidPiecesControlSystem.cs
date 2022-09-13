using UnityEngine;

namespace Model
{
    public class AsteroidPiecesControlSystem : AsteroidControlSystem
    {
        private EnemyControlSystem AsteroidControlSystem => RelatedControlSystem as EnemyControlSystem;

        public AsteroidPiecesControlSystem(EnemySettings asteroidPiecesSettings, GameModelControlSystem asteroidControlSystem) : base(asteroidPiecesSettings, asteroidControlSystem)
        {
        }

        public override void Initialize()
        {
            AsteroidControlSystem.GameModelDestroyed += OnAsteroidDestroyed;
        }

        protected override void OnGameStopped()
        {
            base.OnGameStopped();
            
            AsteroidControlSystem.GameModelDestroyed -= OnAsteroidDestroyed;
        }

        private void OnAsteroidDestroyed(GameModel asteroid, bool totally)
        {
            if (totally)
                return;
            
            var count = Random.Range(EnemySettings.InitialCount, EnemySettings.MaxCount + 1);
            for (var i = 0; i < count; i++)
            {
                CreateGameModel(asteroid.Position, Quaternion.identity);
            }
        }
    }
}