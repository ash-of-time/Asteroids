using System;
using System.Threading.Tasks;
using Tools;
using UnityEngine;

namespace Model
{
    public abstract class GameModelControlSystem : IInitializeSystem, IExecuteSystem
    {
        public readonly GameModelSettings GameModelSettings;
        protected readonly IField Field;
        
        public event Action<GameModel> GameModelCreated;
        public event Action<GameModel> GameModelDestroyed;

        protected GameModelControlSystem(GameModelSettings gameModelSettings, IField field)
        {
            GameModelSettings = gameModelSettings;
            Field = field;

            Game.Instance.GameStopped += OnGameStopped;
        }

        public abstract void Initialize();

        public abstract void Execute();

        protected virtual void OnGameStopped()
        {
            Game.Instance.GameStopped -= OnGameStopped;
        }

        protected abstract GameModel CreateGameModelObject(Vector3 position);

        protected virtual GameModel CreateGameModel(Vector3 position)
        {
            var gameModel = CreateGameModelObject(position);
            gameModel.Destroyed += OnGameModelDestroyed;
            GameModelCreated?.Invoke(gameModel);

            return gameModel;
        }
        
        protected virtual void OnGameModelDestroyed(GameModel gameModel)
        {
            gameModel.Destroyed -= OnGameModelDestroyed;
            GameModelDestroyed?.Invoke(gameModel);
        }
    }
}