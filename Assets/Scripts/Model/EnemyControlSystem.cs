using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public abstract class EnemyControlSystem : GameModelControlSystem
    {
        protected readonly GameModel Player;
        private readonly List<GameModel> _enemyList;
        
        protected EnemySettings EnemySettings => GameModelSettings as EnemySettings;

        protected EnemyControlSystem(EnemySettings enemySettings, IField field, GameModel player) : base(enemySettings, field)
        {
            Player = player;
            _enemyList = new List<GameModel>(enemySettings.MaxCount);
        }

        public override void Initialize()
        {
            for (var i = 0; i < EnemySettings.InitialCount; i++)
            {
                var position = Field.GetRandomPositionFarFromPoint(Player.Position, EnemySettings.PlayerMinimumDistance);
                CreateGameModel(position);
            }
        }

        public override void Execute()
        {
            foreach (var enemy in _enemyList)
            {
                enemy.Update();
            }
        }

        protected override GameModel CreateGameModel(Vector3 position)
        {
            var gameModel = base.CreateGameModel(position);
            _enemyList.Add(gameModel);
            
            return gameModel;
        }

        protected override void OnGameModelDestroyed(GameModel enemy)
        {
            _enemyList.Remove(enemy);
            
            base.OnGameModelDestroyed(enemy);
        }
    }
}