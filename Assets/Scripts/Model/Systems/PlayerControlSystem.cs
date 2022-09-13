using UnityEngine;

namespace Model
{
    public class PlayerControlSystem : GameModelControlSystem
    {
        public Player Player { get; private set; }
        
        private PlayerSettings PlayerSettings => GameModelSettings as PlayerSettings;
        
        public PlayerControlSystem(GameModelSettings gameModelSettings, IField field) : base(gameModelSettings, field)
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
            
            Player?.Destroy();
        }

        protected override GameModel CreateGameModelObject(Vector3 position, Quaternion rotation)
        {
            Player = new Player(position, rotation, PlayerSettings, Field);
            return Player;
        }
        
        protected override void OnGameModelDestroyed(GameModel gameModel)
        {
            Player = null;
            
            base.OnGameModelDestroyed(gameModel);
        }
    }
}