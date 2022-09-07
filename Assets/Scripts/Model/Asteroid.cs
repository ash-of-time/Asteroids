using UnityEngine;

namespace Model
{
    public class Asteroid : Enemy
    {
        public Asteroid(Vector3 position, EnemySettings asteroidSettings) : base(position, asteroidSettings)
        {
        }
    }
}