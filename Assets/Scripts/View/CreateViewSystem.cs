using Model;
using UnityEngine;

namespace View
{
    public class CreateViewSystem
    {
        protected readonly GameModelControlSystem ControlSystem;
        private readonly Game _game;

        public CreateViewSystem(Game game, GameModelControlSystem controlSystem)
        {
            _game = game;
            ControlSystem = controlSystem;

            controlSystem.GameModelCreated += OnModelCreated;
            game.GameStopped += OnGameStooped;
        }
        
        protected virtual void OnModelCreated(GameModel model)
        {
            var presenter = Object.Instantiate(ControlSystem.GameModelSettings.Prefab, model.Position, model.Rotation).GetComponent<Presenter>();
            presenter.Model = model;
        }

        private void OnGameStooped()
        {
            ControlSystem.GameModelCreated -= OnModelCreated;
            _game.GameStopped -= OnGameStooped;
        }
    }
}