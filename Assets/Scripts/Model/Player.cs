namespace Model
{
    public class Player : GameModel
    {
        public Player(PlayerSettings playerSettings, Field field) : base(playerSettings.InitialPosition)
        {
        }
    }
}