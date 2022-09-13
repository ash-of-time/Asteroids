using UnityEngine;

namespace Model
{
    public abstract class Enemy : GameModel
    {
        protected Enemy(Vector3 position, Quaternion rotation, EnemySettings enemySettings, IField field) : base(position, rotation, enemySettings, field)
        {
            
        }

        public override void Collide(GameModel gameModel)
        {
            if (gameModel is Enemy)
                return;
            
            base.Collide(gameModel);
        }
    }
}