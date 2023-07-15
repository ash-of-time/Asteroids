using UnityEngine;

namespace Model
{
    public class PlayerControlSystem : GameModelControlSystem
    {
        public Player Player { get; private set; }
        
        private PlayerSettings PlayerSettings => GameModelSettings as PlayerSettings;
        
        public PlayerControlSystem(Game game, GameModelSettings gameModelSettings) : base(game, gameModelSettings)
        {
        }

        public override void Initialize()
        {
            CreateGameModel(PlayerSettings.InitialPosition, Quaternion.identity);
        }

        public override void Execute()
        {
            Player.Update();
        }

        protected override void OnGameStopped()
        {
            base.OnGameStopped();
            
            Player?.Destroy(true);
        }

        protected override IGameModel CreateGameModelObject(Vector3 position, Quaternion rotation)
        {
            Player = new Player(position, rotation, PlayerSettings, Game.Field);
            return Player;
        }
        
        protected override void OnGameModelDestroyed(IGameModel gameModel, bool totally)
        {
            Player = null;
            
            base.OnGameModelDestroyed(gameModel, totally);
        }
    }
}