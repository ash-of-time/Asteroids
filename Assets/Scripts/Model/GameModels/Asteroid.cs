using UnityEngine;

namespace Model
{
    public class Asteroid : Enemy
    {
        public Asteroid(Vector3 position, Quaternion rotation, EnemySettings asteroidSettings, IField field) : base(position, rotation, asteroidSettings, field)
        {
            MovingStrategy = new StraightMovingStrategy(this);
        }
    }
}