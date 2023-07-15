using UnityEngine;

namespace Model
{
    public class AsteroidPiecesControlSystem : AsteroidControlSystem
    {
        private EnemyControlSystem AsteroidControlSystem => RelatedControlSystem as EnemyControlSystem;

        public AsteroidPiecesControlSystem(Game game, EnemySettings asteroidPiecesSettings,
            GameModelControlSystem asteroidControlSystem) : base(game, asteroidPiecesSettings, asteroidControlSystem)
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

        private void OnAsteroidDestroyed(IGameModel asteroid, bool totally)
        {
            if (totally)
                return;
            
            var count = Random.Range(EnemySettings.InitialCount, EnemySettings.MaxCount + 1);
            for (var i = 0; i < count; i++)
            {
                CreateGameModel(asteroid.Position.Value, Quaternion.identity);
            }
        }
    }
}