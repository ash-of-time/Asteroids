using Tools;

namespace Model
{
    public class PointsCountSystem
    {
        private readonly EnemyControlSystem[] _enemyControlSystems;

        public ReactiveProperty<int> ReactivePoints = new();

        public int Points
        {
            get => ReactivePoints.Get();
            private set => ReactivePoints.Set(value);
        }

        public PointsCountSystem(params EnemyControlSystem[] enemyControlSystems)
        {
            _enemyControlSystems = enemyControlSystems;
            foreach (var system in _enemyControlSystems)
            {
                system.GameModelDestroyed += OnGameModelDestroyed;
            }
            
            Game.Instance.GameStopped += OnGameStopped;
        }

        private void OnGameModelDestroyed(GameModel gameModel)
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