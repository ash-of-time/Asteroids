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
            CreateGameModel(PlayerSettings.InitialPosition);
        }

        public override void Execute()
        {
            Player.Update();
        }

        protected override GameModel CreateGameModelObject(Vector3 position)
        {
            Player = new Player(position, PlayerSettings, Field);
            return Player;
        }
    }
}