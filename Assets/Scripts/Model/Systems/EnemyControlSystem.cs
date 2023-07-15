using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    public abstract class EnemyControlSystem : MultipleGameModelsControlSystem
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        protected EnemySettings EnemySettings => GameModelSettings as EnemySettings;
        
        private Vector3 NewEnemyPosition => Game.Field.GetRandomPositionFarFromPoint(Player.Position.Value, EnemySettings.PlayerMinimumDistance);

        protected EnemyControlSystem(Game game, EnemySettings enemySettings, GameModelControlSystem
            relatedControlSystem) : base(game, enemySettings, relatedControlSystem)
        {
            GameModelsList = new List<IGameModel>(enemySettings.MaxCount);
        }

        public override async void Initialize()
        {
            for (var i = 0; i < EnemySettings.InitialCount; i++)
            {
                CreateGameModel(NewEnemyPosition, Quaternion.identity);
            }

            await CreateEnemiesAsync();
        }

        protected override void OnGameStopped()
        {
            base.OnGameStopped();
            
            _cancellationTokenSource.Cancel();
        }
        
        private async Task CreateEnemiesAsync()
        {
            var token = _cancellationTokenSource.Token;
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(EnemySettings.CreateCooldown * 1000, token);
                    if (GameModelsList.Count < EnemySettings.MaxCount)
                        CreateGameModel(NewEnemyPosition, Quaternion.identity);
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