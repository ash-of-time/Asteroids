using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public abstract class MultipleGameModelsControlSystem : GameModelControlSystem
    {
        protected List<GameModel> _gameModelsList;

        protected MultipleGameModelsControlSystem(GameModelSettings gameModelSettings, IField field) : base(gameModelSettings, field)
        {
        }
        
        public override void Execute()
        {
            for (var i = _gameModelsList.Count - 1; i >= 0; i--)
            {
                _gameModelsList[i].Update();
            }
        }
        
        protected override void OnGameStopped()
        {
            base.OnGameStopped();
            
            for (var i = _gameModelsList.Count - 1; i >= 0; i--)
            {
                _gameModelsList[i].Destroy();
            }
        }
        
        protected override GameModel CreateGameModel(Vector3 position, Quaternion rotation)
        {
            var gameModel = base.CreateGameModel(position, rotation);
            _gameModelsList.Add(gameModel);
            
            return gameModel;
        }
        
        protected override void OnGameModelDestroyed(GameModel gameModel)
        {
            _gameModelsList.Remove(gameModel);
            
            base.OnGameModelDestroyed(gameModel);
        }
    }
}