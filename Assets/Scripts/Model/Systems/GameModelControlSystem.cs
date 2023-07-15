using System;
using Tools;
using UnityEngine;

namespace Model
{
    public abstract class GameModelControlSystem : IInitializeSystem, IExecuteSystem
    {
        public readonly GameModelSettings GameModelSettings;
        protected readonly Game Game;
        
        public event Action<IGameModel> GameModelCreated;
        public event Action<IGameModel, bool> GameModelDestroyed;

        protected GameModelControlSystem(Game game, GameModelSettings gameModelSettings)
        {
            Game = game;
            GameModelSettings = gameModelSettings;

            Game.GameStopped += OnGameStopped;
        }

        public abstract void Initialize();

        public abstract void Execute();

        protected virtual void OnGameStopped()
        {
            Game.GameStopped -= OnGameStopped;
        }

        protected abstract IGameModel CreateGameModelObject(Vector3 position, Quaternion rotation);

        protected virtual IGameModel CreateGameModel(Vector3 position, Quaternion rotation)
        {
            var gameModel = CreateGameModelObject(position, rotation);
            gameModel.Destroyed += OnGameModelDestroyed;
            GameModelCreated?.Invoke(gameModel);

            return gameModel;
        }
        
        protected virtual void OnGameModelDestroyed(IGameModel gameModel, bool totally)
        {
            gameModel.Destroyed -= OnGameModelDestroyed;
            GameModelDestroyed?.Invoke(gameModel, totally);
        }
    }
}