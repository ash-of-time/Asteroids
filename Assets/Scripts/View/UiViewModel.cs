using System;
using Model;
using UnityEngine;

namespace View
{
    public class UiViewModel
    {
        private readonly UI _ui;
        private GameModelControlSystem _playerControlSystem;
        private PointsCountSystem _pointsCountSystem;
        private Player _player;

        public UiViewModel(UI ui)
        {
            _ui = ui;

            Game.Instance.GameStopped += OnGameStopped;
        }

        public void SubscribeOnPlayer(GameModelControlSystem playerControlSystem)
        {
            _playerControlSystem = playerControlSystem;
            _playerControlSystem.GameModelCreated += OnPlayerCreated;
        }
        
        public void SubscribeOnPoints(PointsCountSystem pointsCountSystem)
        {
            _pointsCountSystem = pointsCountSystem;
            _pointsCountSystem.ReactivePoints.Changed += OnPointsChanged;
        }
        
        private void OnGameStopped()
        {
            _playerControlSystem.GameModelCreated -= OnPlayerCreated;
            _pointsCountSystem.ReactivePoints.Changed -= OnPointsChanged;
            _player.ReactivePosition.Changed -= OnPlayerPositionChanged;
            _player.ReactiveRotation.Changed -= OnPlayerRotationChanged;
            _player.ReactiveVelocity.Changed -= OnPlayerVelocityChanged;
            Game.Instance.GameStopped -= OnGameStopped;
        }

        private void OnPlayerCreated(GameModel gameModel)
        {
            if (gameModel is not Player player)
                return;

            _player = player;
            _player.ReactivePosition.Changed += OnPlayerPositionChanged;
            _player.ReactiveRotation.Changed += OnPlayerRotationChanged;
            _player.ReactiveVelocity.Changed += OnPlayerVelocityChanged;
        }

        private void OnPlayerPositionChanged(Vector3 playerPosition)
        {
            var x = Math.Round(playerPosition.x, 1, MidpointRounding.AwayFromZero);
            var z = Math.Round(playerPosition.z, 1, MidpointRounding.AwayFromZero);
            _ui.PositionLabel.text = $"Position: ({x}; {z})";
        }
        
        private void OnPlayerRotationChanged(Quaternion playerRotation)
        {
            var rotation = Math.Round(playerRotation.eulerAngles.y);
            _ui.RotationLabel.text = $"Rotation: {rotation}";
        }
        
        private void OnPlayerVelocityChanged(float playerVelocity)
        {
            var velocity = Math.Round(playerVelocity);
            _ui.VelocityLabel.text = $"Velocity: {velocity}";
        }
        
        private void OnPointsChanged(int points)
        {
            _ui.PointsLabel.text = $"Points: {points}";
        }
    }
}