using Model;

namespace View
{
    public class PlayerPresenter : Presenter
    {
        public IPlayer Player => _model as IPlayer;
    }
}