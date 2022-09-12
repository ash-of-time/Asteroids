using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    public abstract class EnemyControlSystem : GameModelControlSystem
    {
        protected readonly GameModel Player;
        private readonly List<GameModel> _enemyList;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        protected EnemySettings EnemySettings => GameModelSettings as EnemySettings;
        
        private Vector3 NewEnemyPosition => Field.GetRandomPositionFarFromPoint(Player.Position, EnemySettings.PlayerMinimumDistance);

        protected EnemyControlSystem(EnemySettings enemySettings, IField field, GameModel player) : base(enemySettings, field)
        {
            Player = player;
            _enemyList = new List<GameModel>(enemySettings.MaxCount);
        }

        public override async void Initialize()
        {
            for (var i = 0; i < EnemySettings.InitialCount; i++)
            {
                CreateGameModel(NewEnemyPosition);
            }

            await CreateEnemiesAsync();
        }

        public override void Execute()
        {
            foreach (var enemy in _enemyList)
            {
                enemy.Update();
            }
        }

        protected override void OnGameStopped()
        {
            base.OnGameStopped();
            
            _cancellationTokenSource.Cancel();
            
            for (var i = _enemyList.Count - 1; i >= 0; i--)
            {
                _enemyList[i].Destroy();
            }
        }

        protected override GameModel CreateGameModel(Vector3 position)
        {
            var gameModel = base.CreateGameModel(position);
            _enemyList.Add(gameModel);
            
            return gameModel;
        }

        protected override void OnGameModelDestroyed(GameModel gameModel)
        {
            _enemyList.Remove(gameModel);
            
            base.OnGameModelDestroyed(gameModel);
        }
        
        private async Task CreateEnemiesAsync()
        {
            var token = _cancellationTokenSource.Token;
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(EnemySettings.CreateCooldown * 1000, token);
                    if (_enemyList.Count < EnemySettings.MaxCount)
                        CreateGameModel(NewEnemyPosition);
                }
            }
            catch (TaskCanceledException e)
            {
                Debug.Log(e);
            }

            _cancellationTokenSource.Dispose();
        }
    }
}