using Model;

namespace View
{
    public class WelcomeUiPresenter
    {
        private readonly Game _game;
        private readonly WelcomeUiView _view;

        public WelcomeUiPresenter(Game game, WelcomeUiView view)
        {
            _game = game;
            _view = view;
            _view.Presenter = this;
        }

        public void ButtonClick()
        {
            _view.SetActive(false);
            _game.Start();
        }
    }
}