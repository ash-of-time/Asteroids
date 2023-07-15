using System;
using Model;
using UnityEngine;

namespace View
{
    public class Presenter : MonoBehaviour
    {
        protected Game Game;
        private IGameModel _gameModel;

        public event Action<Presenter> ModelDestroyed;

        public IGameModel GameModel => _gameModel;

        public void Initialize(Game game, IGameModel gameModel)
        {
            Game = game;
            _gameModel = gameModel;
            
            if (_gameModel == null)
                return;

            var position = _gameModel.Position;
            var rotation = _gameModel.Rotation;
            PositionChanged(position.Value);
            RotationChanged(rotation.Value);
            position.Changed += PositionChanged;
            rotation.Changed += RotationChanged;
            _gameModel.Destroyed += OnGameModelDestroyed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<Presenter>(out var otherPresenter) || Game.IsStopped)
                return;
            
            _gameModel.Collide(otherPresenter.GameModel);
        }

        private void OnDestroy()
        {
            OnGameModelDestroyed(null, true);
        }

        private void PositionChanged(Vector3 position)
        {
            transform.position = position;
        }

        private void RotationChanged(Quaternion rotation)
        {
            transform.rotation = rotation;
        }
        
        private void OnGameModelDestroyed(IGameModel model, bool totally)
        {
            if (_gameModel == null)
                return;
            
            _gameModel.Position.Changed -= PositionChanged;
            _gameModel.Rotation.Changed -= RotationChanged;
            _gameModel.Destroyed -= OnGameModelDestroyed;

            _gameModel = null;
            
            if (Game != null && !Game.IsDestroyed)
                ModelDestroyed?.Invoke(this);
        }
    }
}