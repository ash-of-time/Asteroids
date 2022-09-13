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
            _game.MultipleGameModelsControlSystemCreated += OnMultipleGameModelsControlSystemCreated;
            
            _game.Initialize();
        }

        private void OnPlayerControlSystemCreated(GameModelControlSystem system)
        {
            new ViewControlSystem(_game, system);
        }
        
        private void OnMultipleGameModelsControlSystemCreated(GameModelControlSystem system)
        {
            new ViewControlSystemWithPool(_game, system);
        }

        private void Update()
        {
            if (!_game.Stopped)
                _game.Execute();
        }

        private void OnDestroy()
        {
            _game.PlayerControlSystemCreated -= OnPlayerControlSystemCreated;
            _game.MultipleGameModelsControlSystemCreated -= OnMultipleGameModelsControlSystemCreated;
            _game.Stop(true);
        }
    }
}