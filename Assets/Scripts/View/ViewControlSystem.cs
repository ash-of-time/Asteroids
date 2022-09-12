using Model;
using UnityEngine;

namespace View
{
    public class ViewControlSystem
    {
        protected readonly GameModelControlSystem ControlSystem;
        private readonly Game _game;

        public ViewControlSystem(Game game, GameModelControlSystem controlSystem)
        {
            _game = game;
            ControlSystem = controlSystem;

            controlSystem.GameModelCreated += OnModelCreated;
            game.GameStopped += OnGameStooped;
        }
        
        protected virtual void OnModelCreated(GameModel gameModel)
        {
            var presenter = Object.Instantiate(ControlSystem.GameModelSettings.Prefab, gameModel.Position, gameModel.Rotation).GetComponent<Presenter>();
            presenter.GameModel = gameModel;
            presenter.ModelDestroyed += OnModelDestroyed;
        }
        
        protected virtual void OnModelDestroyed(Presenter presenter)
        {
            presenter.ModelDestroyed -= OnModelDestroyed;
            Object.Destroy(presenter.gameObject);
        }

        private void OnGameStooped()
        {
            ControlSystem.GameModelCreated -= OnModelCreated;
            _game.GameStopped -= OnGameStooped;
        }
    }
}