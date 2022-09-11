using Model;

namespace View
{
    public class PlayerPresenter : Presenter
    {
        public IPlayer Player => GameModel as IPlayer;
    }
}