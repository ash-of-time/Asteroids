﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    public abstract class EnemyControlSystem : MultipleGameModelsControlSystem
    {
        protected readonly GameModel Player;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        protected EnemySettings EnemySettings => GameModelSettings as EnemySettings;
        
        private Vector3 NewEnemyPosition => Field.GetRandomPositionFarFromPoint(Player.Position, EnemySettings.PlayerMinimumDistance);

        protected EnemyControlSystem(EnemySettings enemySettings, IField field, GameModel player) : base(enemySettings, field)
        {
            Player = player;
            _gameModelsList = new List<GameModel>(enemySettings.MaxCount);
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
                    if (_gameModelsList.Count < EnemySettings.MaxCount)
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