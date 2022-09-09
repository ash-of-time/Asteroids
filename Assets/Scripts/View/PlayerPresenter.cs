using Model;

namespace View
{
    public class PlayerPresenter : Presenter
    {
        public Player Player => _model as Player;
    }
}