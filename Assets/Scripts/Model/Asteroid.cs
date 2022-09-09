using UnityEngine;

namespace Model
{
    public class Asteroid : Enemy
    {
        public Asteroid(Vector3 position, EnemySettings asteroidSettings) : base(position, asteroidSettings)
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