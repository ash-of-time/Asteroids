using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class ProjectileControlSystem : MultipleGameModelsControlSystem
    {
        public ProjectileControlSystem(GameModelSettings gameModelSettings, PlayerControlSystem playerControlSystem) : base(gameModelSettings, playerControlSystem)
        {
            GameModelsList = new List<GameModel>();
        }

        public override void Initialize()
        {
            Player.Fired += OnPlayerFired;
        }

        protected override void OnGameStopped()
        {
            base.OnGameStopped();
            
            Player.Fired -= OnPlayerFired;
        }

        protected override GameModel CreateGameModelObject(Vector3 position, Quaternion rotation)
        {
            return new Projectile(position, rotation, GameModelSettings, Field.Instance);
        }

        private void OnPlayerFired()
        {
            base.CreateGameModel(Player.BarrelPosition, Player.Rotation);
        }
    }
}