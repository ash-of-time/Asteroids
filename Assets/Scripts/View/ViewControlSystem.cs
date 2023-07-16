using Model;
using UnityEngine;

namespace View
{
    public abstract class ViewControlSystem
    {
        protected readonly Game Game;
        protected readonly GameModelControlSystem ControlSystem;

        public ViewControlSystem(Game game, GameModelControlSystem controlSystem)
        {
            Game = game;
            ControlSystem = controlSystem;

            controlSystem.GameModelCreated += OnModelCreated;
            Game.GameStopped += OnGameStopped;
        }

        protected abstract void OnModelCreated(IGameModel gameModel);
        
        protected virtual void OnModelDestroyed(Presenter presenter)
        {
            presenter.ModelDestroyed -= OnModelDestroyed;
            Object.Destroy(presenter.gameObject);
        }

        protected virtual void OnGameStopped()
        {
            ControlSystem.GameModelCreated -= OnModelCreated;
            Game.GameStopped -= OnGameStopped;
        }
    }
}