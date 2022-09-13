using Model;
using UnityEngine;

namespace View
{
    public class GameBehaviour : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private UI _ui;
        
        private Game _game;
        private UiViewModel _uiViewModel;

        private void Start()
        {
            _game = new Game(_gameSettings);
            _uiViewModel = new UiViewModel(_ui);
            
            _game.PlayerControlSystemCreated += OnPlayerControlSystemCreated;
            _game.MultipleGameModelsControlSystemCreated += OnMultipleGameModelsControlSystemCreated;
            _game.PointsCountSystemCreated += OnPointsCountSystemCreated;
            
            _game.Initialize();
        }

        private void OnPlayerControlSystemCreated(GameModelControlSystem system)
        {
            new ViewControlSystem(_game, system);
            _uiViewModel.SubscribeOnPlayer(system);
        }
        
        private void OnMultipleGameModelsControlSystemCreated(GameModelControlSystem system)
        {
            new ViewControlSystemWithPool(_game, system);
        }
        
        private void OnPointsCountSystemCreated(PointsCountSystem pointsCountSystem)
        {
            _uiViewModel.SubscribeOnPoints(pointsCountSystem);
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
            _game.PointsCountSystemCreated -= OnPointsCountSystemCreated;
            _game.Stop(true);
        }
    }
}