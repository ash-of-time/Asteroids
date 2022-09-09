using System;
using Tools;

namespace Model
{
    public class Game : IInitializeSystem, IExecuteSystem
    {
        public Player Player;
        public AsteroidControlSystem AsteroidsControlSystem;
        public SaucerControlSystem SaucersControlSystem;
        
        public readonly GameSettings GameSettings;

        public event Action GameStopped;

        public Game(GameSettings gameSettings)
        {
            GameSettings = gameSettings;
            Initialize();
        }

        public void Initialize()
        {
            var field = new Field(GameSettings.FieldSettings);
            
            Player = new Player(GameSettings.PlayerSettings, field);
            
            AsteroidsControlSystem = new AsteroidControlSystem(GameSettings.AsteroidSettings, field, Player);
            SaucersControlSystem = new SaucerControlSystem(GameSettings.SaucerSettings, field, Player);
        }

        public void Execute()
        {
            Player.Update();
            AsteroidsControlSystem.Execute();
            SaucersControlSystem.Execute();
        }

        public void Start()
        {
            AsteroidsControlSystem.Initialize();
            SaucersControlSystem.Initialize();
        }

        public void Stop()
        {
            GameStopped?.Invoke();
        }
    }
}