using Model;

namespace View
{
    public class GameOverUiPresenter
    {
        private readonly GameOverUiView _view;
        private PointsCountSystem _pointsCountSystem;

        public GameOverUiPresenter(GameOverUiView view)
        {
            _view = view;
            _view.Presenter = this;
            _view.SetActive(false);
            Game.Instance.PointsCountSystemCreated += OnPointsCountSystemCreated;
            Game.Instance.GameStopped += OnGameStopped;
        }

        public void ButtonClick()
        {
            _view.SetActive(false);
            Game.Instance.Start();
        }
        
        private void OnPointsCountSystemCreated(PointsCountSystem pointsCountSystem)
        {
            _pointsCountSystem = pointsCountSystem;
            OnPointsChanged(_pointsCountSystem.Points);
            _pointsCountSystem.ReactivePoints.Changed += OnPointsChanged;
        }
        
        private void OnPointsChanged(int points)
        {
            _view.Points.text = $"You earned {points} points";
        }
        
        private void OnGameStopped()
        {
            _pointsCountSystem.ReactivePoints.Changed -= OnPointsChanged;
            
            if(!Game.Instance.IsDestroyed)
                _view.SetActive(true);
        }
    }
}