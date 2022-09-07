using UnityEngine;

namespace Model
{
    public abstract class Enemy : GameModel
    {
        protected Enemy(Vector3 position, EnemySettings enemySettings) : base(position)
        {
            
        }
    }
}