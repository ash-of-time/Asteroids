using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class ProjectileControlSystem : MultipleGameModelsControlSystem
    {
        protected readonly Player Player;
        
        public ProjectileControlSystem(GameModelSettings gameModelSettings, IField field, Player player) : base(gameModelSettings, field)
        {
            Player = player;
            _gameModelsList = new List<GameModel>();
        }

        public override void Initialize()
        {
            Player.Fired += OnPlayerFired;
        }

        protected override void OnGameStopped()
        {
            Player.Fired -= OnPlayerFired;
        }

        protected override GameModel CreateGameModelObject(Vector3 position, Quaternion rotation)
        {
            return new Projectile(position, GameModelSettings, Field, rotation);
        }

        private void OnPlayerFired()
        {
            base.CreateGameModel(Player.BarrelPosition, Player.Rotation);
        }
    }
}