using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;

namespace View
{
    public class PlayerPresenter : Presenter
    {
        private const int EnemyLayerMask = 1 << 6;
        
        [SerializeField] private GameObject Laser;

        public IPlayer Player => GameModel as Player;
        
        private RaycastHit[] _raycastHits;

        public void AlternativeFire()
        {
            _raycastHits ??= CreateRaycastHitArray();
            Physics.RaycastNonAlloc(Player.Position.Value, Player.ForwardDirection, _raycastHits, Game.Field.Diagonal, EnemyLayerMask);
            var hitModels = _raycastHits.Where(hit => hit.transform != null)
                .Select(enemy => enemy.transform.GetComponent<Presenter>().GameModel);
            if (Player.TryAlternativeFire(hitModels))
            {
                Laser.transform.localScale = new Vector3(1, 1, Game.Field.Diagonal);
                Laser.SetActive(true);
            }
        }

        private RaycastHit[] CreateRaycastHitArray()
        {
            var gameSettings = Game.GameSettings;
            var size = gameSettings.AsteroidSettings.MaxCount +
                       gameSettings.AsteroidPieceSettings.MaxCount +
                       gameSettings.SaucerSettings.MaxCount;
            return new RaycastHit[size];
        }
    }
}