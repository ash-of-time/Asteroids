using Model;
using UnityEngine;

namespace View
{
    public class CreateViewSystem
    {
        private readonly Game _game;

        public CreateViewSystem(Game game)
        {
            _game = game;

            OnModelCreated(game.Player, game.GameSettings.PlayerSettings);
            game.AsteroidsControlSystem.EnemyCreated += OnModelCreated;
            game.SaucersControlSystem.EnemyCreated += OnModelCreated;
            game.GameStopped += OnGameStooped;
        }
        
        private void OnModelCreated(GameModel model, GameModelSettings settings)
        {
            var presenter = Object.Instantiate(settings.Prefab, model.Position, model.Rotation).GetComponent<Presenter>();
            presenter.Model = model;
        }

        private void OnGameStooped()
        {
            _game.AsteroidsControlSystem.EnemyCreated -= OnModelCreated;
            _game.SaucersControlSystem.EnemyCreated -= OnModelCreated;
            _game.GameStopped -= OnGameStooped;
        }
    }
}