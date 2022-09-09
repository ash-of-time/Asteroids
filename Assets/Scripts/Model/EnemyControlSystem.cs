using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Model
{
    public abstract class EnemyControlSystem<T> : IInitializeSystem, IExecuteSystem where T : Enemy
    {
        protected readonly EnemySettings EnemySettings;
        protected readonly Field Field;
        protected readonly Player Player;
        private List<T> _enemyList;

        public event Action<T, GameModelSettings> EnemyCreated;

        protected EnemyControlSystem(EnemySettings enemySettings, Field field, Player player)
        {
            EnemySettings = enemySettings;
            Field = field;
            Player = player;
            _enemyList = new List<T>(enemySettings.MaxCount);
        }

        public void Initialize()
        {
            for (var i = 0; i < EnemySettings.InitialCount; i++)
            {
                CreateEnemy();
            }
        }

        public void Execute()
        {
            foreach (var enemy in _enemyList)
            {
                enemy.Update();
            }
        }

        protected abstract T CreateEnemyObject(Vector3 position, EnemySettings asteroidSettings);

        private void CreateEnemy()
        {
            var position = Field.GetRandomPositionFarFromPoint(Player.Position, EnemySettings.PlayerMinimumDistance);
            var enemy = CreateEnemyObject(position, EnemySettings);
            _enemyList.Add(enemy);
            EnemyCreated?.Invoke(enemy, EnemySettings);
        }
    }
}