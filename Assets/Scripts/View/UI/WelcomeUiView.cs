namespace View
{
    public class WelcomeUiView : UiView
    {
        public WelcomeUiPresenter Presenter { get; set; }

        public void ButtonClick()
        {
            Presenter.ButtonClick();
        }
    }
}