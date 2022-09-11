using System;
using Model;
using UnityEngine;

namespace View
{
    public class Presenter : MonoBehaviour
    {
        protected GameModel GameModel;

        public event Action<Presenter> ModelDestroyed;

        public GameModel Model
        {
            get => GameModel;
            set
            {
                GameModel = value;
                PositionChanged(GameModel.Position);
                RotationChanged(GameModel.Rotation);
                GameModel.ReactivePosition.Changed += PositionChanged;
                GameModel.ReactiveRotation.Changed += RotationChanged;
                GameModel.Destroyed += OnGameModelDestroyed;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<Presenter>(out var otherPresenter))
                return;
            
            Model.Collide(otherPresenter.Model);
        }

        private void PositionChanged(Vector3 position)
        {
            transform.position = position;
        }

        private void RotationChanged(Quaternion rotation)
        {
            transform.rotation = rotation;
        }
        
        private void OnGameModelDestroyed(GameModel model)
        {
            GameModel.ReactivePosition.Changed -= PositionChanged;
            GameModel.ReactiveRotation.Changed -= RotationChanged;
            GameModel.Destroyed -= OnGameModelDestroyed;

            GameModel = null;
            
            ModelDestroyed?.Invoke(this);
        }
    }
}