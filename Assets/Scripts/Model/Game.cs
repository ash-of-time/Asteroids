using System;
using System.Collections.Generic;
using Tools;

namespace Model
{
    public class Game : IExecuteSystem
    {
        private readonly GameSettings _gameSettings;
        private readonly List<GameModelControlSystem> _gameModelControlSystems = new(5);

        public static Game Instance { get; private set; }

        public bool IsStopped { get; private set; }
        public bool IsDestroyed { get; private set; }

        public event Action<GameModelControlSystem> PlayerControlSystemCreated;
        public event Action<MultipleGameModelsControlSystem> MultipleGameModelsControlSystemCreated;
        public event Action<PointsCountSystem> PointsCountSystemCreated;
        public event Action GameStarted;
        public event Action GameStopped;


        private Game(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            Field.CreateInstance(_gameSettings.FieldSettings);
        }

        public static void Initialize(GameSettings gameSettings)
        {
            Instance = new Game(gameSettings);
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
            var playerControlSystem = new PlayerControlSystem(_gameSettings.PlayerSettings);
            playerControlSystem.GameModelDestroyed += OnPlayerDestroyed;
            _gameModelControlSystems.Add(playerControlSystem);

            PlayerControlSystemCreated?.Invoke(playerControlSystem);
            playerControlSystem.Initialize();

            return playerControlSystem;
        }

        private void CreateShootingControlSystem(PlayerControlSystem playerControlSystem)
        {
            CreateMultipleGameModelsControlSystem<ProjectileControlSystem>(_gameSettings.ProjectileSettings, playerControlSystem);
        }

        private void CreateEnemyControlSystems(PlayerControlSystem playerControlSystem)
        {
            var asteroidsControlSystem = CreateMultipleGameModelsControlSystem<AsteroidControlSystem>(_gameSettings.AsteroidSettings, playerControlSystem);
            CreateMultipleGameModelsControlSystem<AsteroidPiecesControlSystem>(_gameSettings.AsteroidPieceSettings, asteroidsControlSystem);
            CreateMultipleGameModelsControlSystem<SaucerControlSystem>(_gameSettings.SaucerSettings, playerControlSystem);
        }

        private void CreatePointsCountSystem()
        {
            var pointsCountSystem = new PointsCountSystem();
            foreach (var controlSystem in _gameModelControlSystems)
            {
                if (controlSystem is EnemyControlSystem enemyControlSystem)
                    pointsCountSystem.Add(enemyControlSystem);
            }

            PointsCountSystemCreated?.Invoke(pointsCountSystem);
        }
        
        private T CreateMultipleGameModelsControlSystem<T>(GameModelSettings gameModelSettings, GameModelControlSystem relatedControlSystem) where T : MultipleGameModelsControlSystem
        {
            var controlSystem = (T)Activator.CreateInstance(typeof(T), gameModelSettings, relatedControlSystem);
            _gameModelControlSystems.Add(controlSystem);
            MultipleGameModelsControlSystemCreated?.Invoke(controlSystem);
            controlSystem.Initialize();

            return controlSystem;
        }

        private void OnPlayerDestroyed(GameModel player, bool totally)
        {
            if (!IsStopped)
                Stop(false);
        }
    }
}