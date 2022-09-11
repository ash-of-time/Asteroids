using Model;
using UnityEngine;

namespace View
{
    public class Presenter : MonoBehaviour
    {
        protected GameModel GameModel;

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
            
            Destroy(this);
        }
    }
}