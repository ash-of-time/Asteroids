using UnityEngine;

namespace Model
{
    public class Asteroid : Enemy
    {
        public Asteroid(Vector3 position, EnemySettings asteroidSettings, IField field) : base(position, asteroidSettings, field)
        {
            Rotation = Quaternion.Euler(0, Random.Range(-180f, 180f), 0);
        }

        protected override void Move()
        {
            Position += ForwardDirection * (Settings.MaxVelocity * Time.deltaTime);
            
            base.Move();
        }
    }
}