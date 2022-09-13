using UnityEngine;

namespace Model
{
    public class PlayerControlSystem : GameModelControlSystem
    {
        public Player Player { get; private set; }
        
        private PlayerSettings PlayerSettings => GameModelSettings as PlayerSettings;
        
        public PlayerControlSystem(GameModelSettings gameModelSettings) : base(gameModelSettings)
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

        protected override GameModel CreateGameModelObject(Vector3 position, Quaternion rotation)
        {
            Player = new Player(position, rotation, PlayerSettings, Field.Instance);
            return Player;
        }
        
        protected override void OnGameModelDestroyed(GameModel gameModel, bool totally)
        {
            Player = null;
            
            base.OnGameModelDestroyed(gameModel, totally);
        }
    }
}