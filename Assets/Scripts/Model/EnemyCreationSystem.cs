using System;
using UnityEngine;

namespace Model
{
    public abstract class EnemyCreationSystem<T> where T : Enemy
    {
        protected readonly EnemySettings EnemySettings;
        protected readonly Field Field;
        protected readonly Player Player;

        public event Action<T, GameModelSettings> EnemyCreated;

        protected EnemyCreationSystem(EnemySettings enemySettings, Field field, Player player)
        {
            EnemySettings = enemySettings;
            Field = field;
            Player = player;
        }

        public void Start()
        {
            for (var i = 0; i < EnemySettings.InitialCount; i++)
            {
                CreateEnemy();
            }
        }

        protected abstract T CreateEnemyObject(Vector3 position, EnemySettings asteroidSettings);

        private void CreateEnemy()
        {
            var position = Field.GetRandomPositionFarFromPoint(Player.Position, EnemySettings.PlayerMinimumDistance);
            var enemy = CreateEnemyObject(position, EnemySettings);
            EnemyCreated?.Invoke(enemy, EnemySettings);
        }
    }
}