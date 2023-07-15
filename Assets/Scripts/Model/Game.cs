using System;
using System.Collections.Generic;
using Tools;

namespace Model
{
    public class Game : IExecuteSystem
    {
        private readonly List<GameModelControlSystem> _gameModelControlSystems = new(5);

        public GameSettings GameSettings { get; private set; }
        public IField Field { get; private set; }
        public bool IsStopped { get; private set; }
        public bool IsDestroyed { get; private set; }

        public event Action<GameModelControlSystem> PlayerControlSystemCreated;
        public event Action<MultipleGameModelsControlSystem> MultipleGameModelsControlSystemCreated;
        public event Action<PointsCountSystem> PointsCountSystemCreated;
        public event Action GameStarted;
        public event Action GameStopped;


        public Game(GameSettings gameSettings)
        {
            GameSettings = gameSettings;
            Field = new Field(GameSettings.FieldSettings);
        }

        public void Start()
        {
            var playerControlSystem = CreatePlayerControlSystem();
            CreateShootingControlSystem(playerControlSystem);
            CreateEnemyControlSystems(playerControlSystem);
            CreatePointsCountSystem();

            IsStopped = false;
            GameStarted?.Invoke();
        }

        public void Execute()
        {
            foreach (var system in _gameModelControlSystems)
            {
                system.Execute();
            }
        }

        public void Stop(bool destroy)
        {
            IsStopped = true;
            IsDestroyed = destroy;
            _gameModelControlSystems.Clear();
            GameStopped?.Invoke();
        }

        private PlayerControlSystem CreatePlayerControlSystem()
        {
            var playerControlSystem = new PlayerControlSystem(this, GameSettings.PlayerSettings);
            playerControlSystem.GameModelDestroyed += OnPlayerDestroyed;
            _gameModelControlSystems.Add(playerControlSystem);

            PlayerControlSystemCreated?.Invoke(playerControlSystem);
            playerControlSystem.Initialize();

            return playerControlSystem;
        }

        private void CreateShootingControlSystem(PlayerControlSystem playerControlSystem)
        {
            CreateMultipleGameModelsControlSystem<ProjectileControlSystem>(GameSettings.ProjectileSettings, playerControlSystem);
        }

        private void CreateEnemyControlSystems(PlayerControlSystem playerControlSystem)
        {
            var asteroidsControlSystem = CreateMultipleGameModelsControlSystem<AsteroidControlSystem>(GameSettings.AsteroidSettings, playerControlSystem);
            CreateMultipleGameModelsControlSystem<AsteroidPiecesControlSystem>(GameSettings.AsteroidPieceSettings, asteroidsControlSystem);
            CreateMultipleGameModelsControlSystem<SaucerControlSystem>(GameSettings.SaucerSettings, playerControlSystem);
        }

        private void CreatePointsCountSystem()
        {
            var pointsCountSystem = new PointsCountSystem(this);
            foreach (var controlSystem in _gameModelControlSystems)
            {
                if (controlSystem is EnemyControlSystem enemyControlSystem)
                    pointsCountSystem.Add(enemyControlSystem);
            }

            PointsCountSystemCreated?.Invoke(pointsCountSystem);
        }
        
        private T CreateMultipleGameModelsControlSystem<T>(GameModelSettings gameModelSettings, GameModelControlSystem relatedControlSystem) where T : MultipleGameModelsControlSystem
        {
            var controlSystem = (T)Activator.CreateInstance(typeof(T), this, gameModelSettings, relatedControlSystem);
            _gameModelControlSystems.Add(controlSystem);
            MultipleGameModelsControlSystemCreated?.Invoke(controlSystem);
            controlSystem.Initialize();

            return controlSystem;
        }

        private void OnPlayerDestroyed(IGameModel player, bool totally)
        {
            if (!IsStopped)
                Stop(false);
        }
    }
}