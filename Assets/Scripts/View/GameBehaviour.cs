using Model;
using UnityEngine;

namespace View
{
    public class GameBehaviour : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;
        
        private void Start()
        {
            var game = new Game(_gameSettings);
            
            var createViewSystem = new CreateViewSystem(game);
            
            game.Start();
        }
    }
}