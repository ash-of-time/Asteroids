using System.Collections.Generic;
using Tools;

namespace Model
{
    public class PointsCountSystem
    {
        private readonly Game _game;
        private readonly List<EnemyControlSystem> _enemyControlSystems = new(3);
        private readonly ReactiveProperty<int> _points = new();

        public IReadOnlyReactiveProperty<int> Points => _points;

        public PointsCountSystem(Game game)
        {
            _game = game;
            foreach (var system in _enemyControlSystems)
            {
                system.GameModelDestroyed += OnGameModelDestroyed;
            }
            
            _game.GameStopped += OnGameStopped;
        }

        public void Add(EnemyControlSystem enemyControlSystem)
        {
            _enemyControlSystems.Add(enemyControlSystem);
            enemyControlSystem.GameModelDestroyed += OnGameModelDestroyed;
        }

        private void OnGameModelDestroyed(IGameModel gameModel, bool totally)
        {
            if (gameModel is Enemy enemy)
                _points.Value += enemy.Points;
        }
        
        private void OnGameStopped()
        {
            foreach (var system in _enemyControlSystems)
            {
                system.GameModelDestroyed -= OnGameModelDestroyed;
            }
            
            _game.GameStopped -= OnGameStopped;
        }
    }
}