using System;
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
        }

        public abstract void Initialize();

        public abstract void Execute();
        
        protected abstract GameModel CreateGameModelObject(Vector3 position);

        protected virtual GameModel CreateGameModel(Vector3 position)
        {
            var gameModel = CreateGameModelObject(position);
            gameModel.Destroyed += OnGameModelDestroyed;
            GameModelCreated?.Invoke(gameModel);

            return gameModel;
        }
        
        private void OnGameModelDestroyed(GameModel enemy)
        {
            enemy.Destroyed -= OnGameModelDestroyed;
            GameModelDestroyed?.Invoke(enemy);
        }
    }
}