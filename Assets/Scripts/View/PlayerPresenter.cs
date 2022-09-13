using System;
using System.Collections.Generic;
using Model;
using UnityEngine;

namespace View
{
    public class PlayerPresenter : Presenter
    {
        private const int EnemyLayerMask = 1 << 6;
        [SerializeField] private GameObject Laser;

        public Player Player => GameModel as Player;

        public void AlternativeFire()
        {
            var hits = Physics.RaycastAll(Player.Position, Player.ForwardDirection, Field.Instance.Diagonal, EnemyLayerMask);
            var hitModelsList = new List<GameModel>();
            foreach (var enemy in hits)
            {
                var model = enemy.transform.GetComponent<Presenter>().GameModel;
                hitModelsList.Add(model);
            }

            if (Player.TryAlternativeFire(hitModelsList))
            {
                Laser.transform.localScale = new Vector3(1, 1, Field.Instance.Diagonal);
                Laser.SetActive(true);
            }
        }
    }
}