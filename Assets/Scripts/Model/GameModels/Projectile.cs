using UnityEngine;

namespace Model
{
    public class Projectile : GameModel
    {
        public Projectile(Vector3 position, Quaternion rotation, GameModelSettings settings, IField field) : base(position, rotation, settings, field)
        {
        }

        protected override void Move()
        {
            Position.Value += ForwardDirection * (Settings.MaxVelocity * Time.deltaTime);

            if (Field.IsPointOutOfField(Position.Value))
                Destroy(false);
        }
    }
}