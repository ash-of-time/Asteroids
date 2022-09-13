using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public abstract class MultipleGameModelsControlSystem : GameModelControlSystem
    {
        protected readonly GameModelControlSystem RelatedControlSystem;
        protected List<GameModel> GameModelsList;

        protected readonly Player Player;

        protected MultipleGameModelsControlSystem(GameModelSettings gameModelSettings, GameModelControlSystem relatedControlSystem) :
            base(gameModelSettings)
        {
            RelatedControlSystem = relatedControlSystem;
            if (RelatedControlSystem is PlayerControlSystem playerControlSystem)
                Player = playerControlSystem.Player;
        }

        public override void Execute()
        {
            for (var i = GameModelsList.Count - 1; i >= 0; i--)
            {
                GameModelsList[i].Update();
            }
        }

        protected override void OnGameStopped()
        {
            base.OnGameStopped();

            for (var i = GameModelsList.Count - 1; i >= 0; i--)
            {
                GameModelsList[i].Destroy(true);
            }
        }

        protected override GameModel CreateGameModel(Vector3 position, Quaternion rotation)
        {
            var gameModel = base.CreateGameModel(position, rotation);
            GameModelsList.Add(gameModel);

            return gameModel;
        }

        protected override void OnGameModelDestroyed(GameModel gameModel, bool totally)
        {
            GameModelsList.Remove(gameModel);

            base.OnGameModelDestroyed(gameModel, totally);
        }
    }
}