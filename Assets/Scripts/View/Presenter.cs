﻿using System;
using Model;
using UnityEngine;

namespace View
{
    public class Presenter : MonoBehaviour
    {
        private GameModel _gameModel;

        public event Action<Presenter> ModelDestroyed;

        public GameModel GameModel
        {
            get => _gameModel;
            set
            {
                _gameModel = value;
                
                if (_gameModel == null)
                    return;
                
                PositionChanged(_gameModel.Position);
                RotationChanged(_gameModel.Rotation);
                _gameModel.ReactivePosition.Changed += PositionChanged;
                _gameModel.ReactiveRotation.Changed += RotationChanged;
                _gameModel.Destroyed += OnGameModelDestroyed;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<Presenter>(out var otherPresenter))
                return;
            
            if(_gameModel == null || otherPresenter._gameModel == null)
                return;
            
            _gameModel.Collide(otherPresenter.GameModel);
            otherPresenter.GameModel.Collide(_gameModel);
        }

        private void OnDestroy()
        {
            OnGameModelDestroyed(null);
        }

        private void PositionChanged(Vector3 position)
        {
            transform.position = position;
        }

        private void RotationChanged(Quaternion rotation)
        {
            transform.rotation = rotation;
        }
        
        private void OnGameModelDestroyed(GameModel _)
        {
            if (_gameModel == null)
                return;
            
            _gameModel.ReactivePosition.Changed -= PositionChanged;
            _gameModel.ReactiveRotation.Changed -= RotationChanged;
            _gameModel.Destroyed -= OnGameModelDestroyed;

            _gameModel = null;
            
            if (Game.Instance != null && !Game.Instance.Destroyed)
                ModelDestroyed?.Invoke(this);
        }
    }
}