using Model;
using UnityEngine;

namespace View
{
    public class CreateViewSystem
    {
        public CreateViewSystem(Game game)
        {
            OnModelCreated(game.Player, game.GameSettings.PlayerSettings);
            game.AsteroidsCreationSystem.EnemyCreated += OnModelCreated;
            game.SaucersCreationSystem.EnemyCreated += OnModelCreated;
        }
        
        private void OnModelCreated(GameModel model, GameModelSettings settings)
        {
            Object.Instantiate(settings.Prefab, model.Position, model.Rotation);
        }
    }
}