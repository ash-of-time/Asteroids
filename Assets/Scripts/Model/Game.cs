using System;
using System.Collections.Generic;
using Tools;

namespace Model
{
    public class Game : IExecuteSystem
    {
        public readonly GameSettings GameSettings;
        private Field _field;
        private List<IExecuteSystem> _executeSystems = new(5); 
        
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
            GameSettings = gameSettings;
            _field = new Field(GameSettings.FieldSettings);
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
            
            IsStopped = false;
            GameStarted?.Invoke();
        }

        public void Execute()
        {
            foreach (var system in _executeSystems)
            {
                system.Execute();
            }
        }

        public void Stop(bool destroy)
        {
            IsStopped = true;
            IsDestroyed = destroy;
            _executeSystems.Clear();
            GameStopped?.Invoke();
        }
        
        private PlayerControlSystem CreatePlayerControlSystem()
        {
            var playerControlSystem = new PlayerControlSystem(GameSettings.PlayerSettings, _field);
            playerControlSystem.GameModelDestroyed += OnPlayerDestroyed;
            _executeSystems.Add(playerControlSystem);
            
            PlayerControlSystemCreated?.Invoke(playerControlSystem);
            playerControlSystem.Initialize();
            
            return playerControlSystem;
        }

        private void CreateShootingControlSystem(PlayerControlSystem playerControlSystem)
        {
            var shootingControlSystem = new ProjectileControlSystem(GameSettings.ProjectileSettings, _field, playerControlSystem.Player);
            _executeSystems.Add(shootingControlSystem);
            MultipleGameModelsControlSystemCreated?.Invoke(shootingControlSystem);
            shootingControlSystem.Initialize();
        }

        private void CreateEnemyControlSystems(PlayerControlSystem playerControlSystem)
        {
            var asteroidsControlSystem = new AsteroidControlSystem(GameSettings.AsteroidSettings, _field, playerControlSystem.Player);
            _executeSystems.Add(asteroidsControlSystem);
            MultipleGameModelsControlSystemCreated?.Invoke(asteroidsControlSystem);
            asteroidsControlSystem.Initialize();
            
            var asteroidPiecesControlSystem = new AsteroidPiecesControlSystem(asteroidsControlSystem, GameSettings.AsteroidPieceSettings, _field); // todo change AsteroidPiecesControlSystem to unify
            _executeSystems.Add(asteroidPiecesControlSystem);
            MultipleGameModelsControlSystemCreated?.Invoke(asteroidPiecesControlSystem);
            asteroidPiecesControlSystem.Initialize();

            var saucersControlSystem = new SaucerControlSystem(GameSettings.SaucerSettings, _field, playerControlSystem.Player);
            _executeSystems.Add(saucersControlSystem);
            MultipleGameModelsControlSystemCreated?.Invoke(saucersControlSystem);
            saucersControlSystem.Initialize();

            var pointsCountSystem = new PointsCountSystem(asteroidsControlSystem, asteroidPiecesControlSystem, saucersControlSystem);
            PointsCountSystemCreated?.Invoke(pointsCountSystem);
        }

        private void OnPlayerDestroyed(GameModel player)
        {
            if (!IsStopped)
                Stop(false);
        }
    }
}