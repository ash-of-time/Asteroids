using Model;
using UnityEngine;

namespace View
{
    public class Presenter : MonoBehaviour
    {
        protected GameModel _model;

        public GameModel Model
        {
            get => _model;
            set
            {
                _model = value;
                _model.ReactivePosition.Changed += PositionChanged;
                _model.ReactiveRotation.Changed += RotationChanged;
            }
        }
        
        private void OnDestroy()
        {
            _model.ReactivePosition.Changed -= PositionChanged;
            _model.ReactiveRotation.Changed -= RotationChanged;
        }

        private void PositionChanged(Vector3 position)
        {
            transform.position = position;
        }

        private void RotationChanged(Quaternion rotation)
        {
            transform.rotation = rotation;
        }
    }
}