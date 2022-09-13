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

        private void Start()
        {
            AdjustFieldSize();

            Game.Initialize(_gameSettings);
            
            CreateUi();
            
            Game.Instance.PlayerControlSystemCreated += OnPlayerControlSystemCreated;
            Game.Instance.MultipleGameModelsControlSystemCreated += OnMultipleGameModelsControlSystemCreated;
        }

        private void AdjustFieldSize()
        {
            var mainCamera = Camera.main;
            if (mainCamera != null)
                _gameSettings.FieldSettings.Size = new Vector2(mainCamera.orthographicSize * 2 * mainCamera.aspect, mainCamera.orthographicSize * 2);
        }

        private void CreateUi()
        {
            new WelcomeUiPresenter(_welcomeUiView);
            new HudPresenter(_hudView);
            new GameOverUiPresenter(_gameOverUiView);
        }

        private void OnPlayerControlSystemCreated(GameModelControlSystem system)
        {
            new ViewControlSystem(system);
        }
        
        private void OnMultipleGameModelsControlSystemCreated(GameModelControlSystem system)
        {
            new ViewControlSystemWithPool(system);
        }

        private void Update()
        {
            if (!Game.Instance.IsStopped)
                Game.Instance.Execute();
        }

        private void OnDestroy()
        {
            Game.Instance.PlayerControlSystemCreated -= OnPlayerControlSystemCreated;
            Game.Instance.MultipleGameModelsControlSystemCreated -= OnMultipleGameModelsControlSystemCreated;
            Game.Instance.Stop(true);
        }
    }
}