using Model;
using UnityEngine;

namespace View
{
    public class GameBehaviour : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;
        private Game _game;

        private void Start()
        {
            _game = new Game(_gameSettings);

            _game.PlayerControlSystemCreated += OnPlayerControlSystemCreated;
            _game.EnemyControlSystemCreated += OnEnemyControlSystemCreated;
            
            _game.Initialize();
        }

        private void OnPlayerControlSystemCreated(GameModelControlSystem system)
        {
            new CreateViewSystem(_game, system);
        }
        
        private void OnEnemyControlSystemCreated(GameModelControlSystem system)
        {
            new CreateViewSystemWithPool(_game, system);
        }

        private void Update()
        {
            _game.Execute();
        }

        private void OnDestroy()
        {
            _game.PlayerControlSystemCreated -= OnPlayerControlSystemCreated;
            _game.EnemyControlSystemCreated -= OnEnemyControlSystemCreated;
            _game.Stop();
        }
    }
}