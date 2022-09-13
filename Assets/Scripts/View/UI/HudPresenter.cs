using System;
using Model;
using UnityEngine;

namespace View
{
    public class HudPresenter
    {
        private readonly HudView _view;
        private GameModelControlSystem _playerControlSystem;
        private PointsCountSystem _pointsCountSystem;
        private Player _player;

        public HudPresenter(HudView view)
        {
            _view = view;
            _view.SetActive(false);

            Game.Instance.PlayerControlSystemCreated += OnPlayerControlSystemCreated;
            Game.Instance.PointsCountSystemCreated += OnPointsCountSystemCreated;
            Game.Instance.GameStarted += OnGameStarted;
            Game.Instance.GameStopped += OnGameStopped;
        }

        private void OnPlayerControlSystemCreated(GameModelControlSystem playerControlSystem)
        {
            _playerControlSystem = playerControlSystem;
            _playerControlSystem.GameModelCreated += OnPlayerCreated;
        }
        
        private void OnPointsCountSystemCreated(PointsCountSystem pointsCountSystem)
        {
            _pointsCountSystem = pointsCountSystem;
            OnPointsChanged(_pointsCountSystem.Points);
            _pointsCountSystem.ReactivePoints.Changed += OnPointsChanged;
        }
        
        private void OnGameStarted()
        {
            _view.SetActive(true);
        }
        
        private void OnGameStopped()
        {
            if (!Game.Instance.IsDestroyed)
                _view.SetActive(false);
            
            _playerControlSystem.GameModelCreated -= OnPlayerCreated;
            _pointsCountSystem.ReactivePoints.Changed -= OnPointsChanged;
            _player.ReactivePosition.Changed -= OnPlayerPositionChanged;
            _player.ReactiveRotation.Changed -= OnPlayerRotationChanged;
            _player.ReactiveVelocity.Changed -= OnPlayerVelocityChanged;
        }

        private void OnPlayerCreated(GameModel gameModel)
        {
            if (gameModel is not Player player)
                return;

            _player = player;
            
            OnPlayerPositionChanged(_player.Position);
            OnPlayerRotationChanged(_player.Rotation);
            OnPlayerVelocityChanged(_player.Velocity);
            _player.ReactivePosition.Changed += OnPlayerPositionChanged;
            _player.ReactiveRotation.Changed += OnPlayerRotationChanged;
            _player.ReactiveVelocity.Changed += OnPlayerVelocityChanged;
        }

        private void OnPlayerPositionChanged(Vector3 playerPosition)
        {
            var x = Math.Round(playerPosition.x, 1, MidpointRounding.AwayFromZero);
            var z = Math.Round(playerPosition.z, 1, MidpointRounding.AwayFromZero);
            _view.PositionLabel.text = $"Position: ({x}; {z})";
        }
        
        private void OnPlayerRotationChanged(Quaternion playerRotation)
        {
            var rotation = Math.Round(playerRotation.eulerAngles.y);
            _view.RotationLabel.text = $"Rotation: {rotation}";
        }
        
        private void OnPlayerVelocityChanged(float playerVelocity)
        {
            var velocity = Math.Round(playerVelocity);
            _view.VelocityLabel.text = $"Velocity: {velocity}";
        }
        
        private void OnPointsChanged(int points)
        {
            _view.PointsLabel.text = $"Points: {points}";
        }
    }
}