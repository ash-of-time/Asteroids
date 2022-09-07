namespace Model
{
    public class Game
    {
        public Player Player;
        public AsteroidCreationSystem AsteroidsCreationSystem;
        public SaucerCreationSystem SaucersCreationSystem;
        
        public readonly GameSettings GameSettings;

        public Game(GameSettings gameSettings)
        {
            GameSettings = gameSettings;
            Init();
        }

        public void Init()
        {
            var field = new Field(GameSettings.FieldSettings);
            
            Player = new Player(GameSettings.PlayerSettings, field);
            
            AsteroidsCreationSystem = new AsteroidCreationSystem(GameSettings.AsteroidSettings, field, Player);
            SaucersCreationSystem = new SaucerCreationSystem(GameSettings.SaucerSettings, field, Player);
        }

        public void Start()
        {
            AsteroidsCreationSystem.Start();
            SaucersCreationSystem.Start();
        }
    }
}