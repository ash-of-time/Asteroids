using UnityEngine;

namespace Model
{
    public class Projectile : GameModel
    {
        public Projectile(Vector3 position, GameModelSettings settings, IField field, Quaternion rotation) : base(position, rotation, settings, field)
        {
        }

        public Projectile(Vector3 position, Quaternion rotation, GameModelSettings settings, IField field) : base(position, rotation, settings, field)
        {
        }


        protected override void Move()
        {
            Position += ForwardDirection * (Settings.MaxVelocity * Time.deltaTime);

            if (_field.IsPointOutOfField(Position))
                Destroy();
        }
    }
}