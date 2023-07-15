using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class ProjectileControlSystem : MultipleGameModelsControlSystem
    {
        public ProjectileControlSystem(Game game, GameModelSettings gameModelSettings,
            PlayerControlSystem playerControlSystem) : base(game, gameModelSettings, playerControlSystem)
        {
            GameModelsList = new List<IGameModel>();
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

        protected override IGameModel CreateGameModelObject(Vector3 position, Quaternion rotation)
        {
            return new Projectile(position, rotation, GameModelSettings, Game.Field);
        }

        private void OnPlayerFired()
        {
            base.CreateGameModel(Player.BarrelPosition, Player.Rotation.Value);
        }
    }
}