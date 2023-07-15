using Model;
using UnityEngine;

namespace View
{
    public class GameBehaviour : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private WelcomeUiView _welcomeUiView;
        [SerializeField] private HudView _hudView;
        [SerializeField] private GameOverUiView _gameOverUiView;

        private Game _game;

        private void Start()
        {
            AdjustFieldSize();

            _game = new Game(_gameSettings);
            
            CreateUi();
            
            _game.PlayerControlSystemCreated += OnPlayerControlSystemCreated;
            _game.MultipleGameModelsControlSystemCreated += OnMultipleGameModelsControlSystemCreated;
        }

        private void AdjustFieldSize()
        {
            var mainCamera = Camera.main;
            if (mainCamera != null)
                _gameSettings.FieldSettings.Size = new Vector2(mainCamera.orthographicSize * 2 * mainCamera.aspect, mainCamera.orthographicSize * 2);
        }

        private void CreateUi()
        {
            new WelcomeUiPresenter(_game, _welcomeUiView);
            new HudPresenter(_game, _hudView);
            new GameOverUiPresenter(_game, _gameOverUiView);
        }

        private void OnPlayerControlSystemCreated(GameModelControlSystem system)
        {
            new PlayerViewControlSystem(_game, system);
        }
        
        private void OnMultipleGameModelsControlSystemCreated(GameModelControlSystem system)
        {
            new ViewControlSystemWithPool(_game, system);
        }

        private void Update()
        {
            if (!_game.IsStopped)
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