using UnityEngine;

namespace Model
{
    public abstract class Enemy : GameModel
    {
        protected Enemy(Vector3 position, EnemySettings enemySettings, IField field) : base(position, enemySettings, field)
        {
            
        }
    }
}