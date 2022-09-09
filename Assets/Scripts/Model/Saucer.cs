using UnityEngine;

namespace Model
{
    public class Saucer : Enemy
    {
        public Saucer(Vector3 position, EnemySettings saucerSettings) : base(position, saucerSettings)
        {
        }

        protected override void Move()
        {
            // throw new System.NotImplementedException();
        }

        protected override void Rotate()
        {
            // throw new System.NotImplementedException();
        }
    }
}