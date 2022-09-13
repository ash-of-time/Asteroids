using Model;

namespace View
{
    public class WelcomeUiPresenter
    {
        private readonly WelcomeUiView _view;

        public WelcomeUiPresenter(WelcomeUiView view)
        {
            _view = view;
            _view.Presenter = this;
        }

        public void ButtonClick()
        {
            _view.SetActive(false);
            Game.Instance.Start();
        }
    }
}