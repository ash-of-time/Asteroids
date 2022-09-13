using System;
using Tools;
using UnityEngine;

namespace Model
{
    public abstract class GameModelControlSystem : IInitializeSystem, IExecuteSystem
    {
        public readonly GameModelSettings GameModelSettings;
        
        public event Action<GameModel> GameModelCreated;
        public event Action<GameModel, bool> GameModelDestroyed;

        protected GameModelControlSystem(GameModelSettings gameModelSettings)
        {
            GameModelSettings = gameModelSettings;

            Game.Instance.GameStopped += OnGameStopped;
        }

        public abstract void Initialize();

        public abstract void Execute();

        protected virtual void OnGameStopped()
        {
            Game.Instance.GameStopped -= OnGameStopped;
        }

        protected abstract GameModel CreateGameModelObject(Vector3 position, Quaternion rotation);

        protected virtual GameModel CreateGameModel(Vector3 position, Quaternion rotation)
        {
            var gameModel = CreateGameModelObject(position, rotation);
            gameModel.Destroyed += OnGameModelDestroyed;
            GameModelCreated?.Invoke(gameModel);

            return gameModel;
        }
        
        protected virtual void OnGameModelDestroyed(GameModel gameModel, bool totally)
        {
            gameModel.Destroyed -= OnGameModelDestroyed;
            GameModelDestroyed?.Invoke(gameModel, totally);
        }
    }
}