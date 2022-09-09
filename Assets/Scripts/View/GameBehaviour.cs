using System;
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
            
            var createViewSystem = new CreateViewSystem(_game);
            
            _game.Start();
        }

        private void Update()
        {
            _game.Execute();
        }

        private void OnDestroy()
        {
            _game.Stop();
        }
    }
}