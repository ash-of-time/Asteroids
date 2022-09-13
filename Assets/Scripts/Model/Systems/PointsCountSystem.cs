using System.Collections.Generic;
using Tools;

namespace Model
{
    public class PointsCountSystem
    {
        private readonly List<EnemyControlSystem> _enemyControlSystems = new(3);

        public ReactiveProperty<int> ReactivePoints { get; } = new();

        public int Points
        {
            get => ReactivePoints.Get();
            private set => ReactivePoints.Set(value);
        }

        public PointsCountSystem()
        {
            foreach (var system in _enemyControlSystems)
            {
                system.GameModelDestroyed += OnGameModelDestroyed;
            }
            
            Game.Instance.GameStopped += OnGameStopped;
        }

        public void Add(EnemyControlSystem enemyControlSystem)
        {
            _enemyControlSystems.Add(enemyControlSystem);
            enemyControlSystem.GameModelDestroyed += OnGameModelDestroyed;
        }

        private void OnGameModelDestroyed(GameModel gameModel, bool totally)
        {
            if (gameModel is Enemy enemy)
                Points += enemy.Points;
        }
        
        private void OnGameStopped()
        {
            foreach (var system in _enemyControlSystems)
            {
                system.GameModelDestroyed -= OnGameModelDestroyed;
            }
            
            Game.Instance.GameStopped -= OnGameStopped;
        }
    }
}