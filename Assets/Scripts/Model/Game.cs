using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools;

namespace Model
{
    public class Game : IInitializeSystem, IExecuteSystem
    {
        public readonly GameSettings GameSettings;
        private Field _field;
        private List<IExecuteSystem> _executeSystems = new(); 
        
        public static Game Instance { get; private set; }
        
        public bool Stopped { get; private set; }
        public bool Destroyed { get; private set; }

        public event Action<GameModelControlSystem> PlayerControlSystemCreated;
        public event Action<GameModelControlSystem> EnemyControlSystemCreated;
        public event Action GameStopped;
        

        public Game(GameSettings gameSettings)
        {
            Instance = this;
            GameSettings = gameSettings;
            _field = new Field(GameSettings.FieldSettings);
        }

        public void Initialize()
        {
            var playerControlSystem = CreatePlayerControlSystem();
            CreateEnemyControlSystems(playerControlSystem);
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
            Stopped = true;
            Destroyed = true;
            GameStopped?.Invoke();
            Instance = null;
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

        private void CreateEnemyControlSystems(PlayerControlSystem playerControlSystem)
        {
            var asteroidsControlSystem = new AsteroidControlSystem(GameSettings.AsteroidSettings, _field, playerControlSystem.Player);
            _executeSystems.Add(asteroidsControlSystem);
            EnemyControlSystemCreated?.Invoke(asteroidsControlSystem);
            asteroidsControlSystem.Initialize();
            
            var asteroidPiecesControlSystem = new AsteroidPiecesControlSystem(asteroidsControlSystem, GameSettings.AsteroidPieceSettings, _field); // todo change AsteroidPiecesControlSystem to unify
            _executeSystems.Add(asteroidPiecesControlSystem);
            EnemyControlSystemCreated?.Invoke(asteroidPiecesControlSystem);
            asteroidPiecesControlSystem.Initialize();

            var saucersControlSystem = new SaucerControlSystem(GameSettings.SaucerSettings, _field, playerControlSystem.Player);
            _executeSystems.Add(saucersControlSystem);
            EnemyControlSystemCreated?.Invoke(saucersControlSystem);
            saucersControlSystem.Initialize();
        }

        private void OnPlayerDestroyed(GameModel player)
        {
            if (!Stopped)
                Stop(false);
        }
    }
}