using Model;

namespace View
{
    public class GameOverUiPresenter
    {
        private readonly Game _game;
        private readonly GameOverUiView _view;
        private PointsCountSystem _pointsCountSystem;

        public GameOverUiPresenter(Game game, GameOverUiView view)
        {
            _game = game;
            _view = view;
            _view.Presenter = this;
            _view.SetActive(false);
            _game.PointsCountSystemCreated += OnPointsCountSystemCreated;
            _game.GameStopped += OnGameStopped;
        }

        public void ButtonClick()
        {
            _view.SetActive(false);
            _game.Start();
        }
        
        private void OnPointsCountSystemCreated(PointsCountSystem pointsCountSystem)
        {
            _pointsCountSystem = pointsCountSystem;
            OnPointsChanged(_pointsCountSystem.Points.Value);
            _pointsCountSystem.Points.Changed += OnPointsChanged;
        }
        
        private void OnPointsChanged(int points)
        {
            _view.Points.text = $"You earned {points} points";
        }
        
        private void OnGameStopped()
        {
            _pointsCountSystem.Points.Changed -= OnPointsChanged;
            
            if(!_game.IsDestroyed)
                _view.SetActive(true);
        }
    }
}