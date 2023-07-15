using System;
using Model;
using UnityEngine;

namespace View
{
    public class HudPresenter
    {
        private readonly Game _game;
        private readonly HudView _view;
        private GameModelControlSystem _playerControlSystem;
        private PointsCountSystem _pointsCountSystem;
        private Player _player;

        public HudPresenter(Game game, HudView view)
        {
            _game = game;
            _view = view;
            _view.SetActive(false);

            _game.PlayerControlSystemCreated += OnPlayerControlSystemCreated;
            _game.PointsCountSystemCreated += OnPointsCountSystemCreated;
            _game.GameStarted += OnGameStarted;
            _game.GameStopped += OnGameStopped;
        }

        private void OnPlayerControlSystemCreated(GameModelControlSystem playerControlSystem)
        {
            _playerControlSystem = playerControlSystem;
            _playerControlSystem.GameModelCreated += OnPlayerCreated;
        }
        
        private void OnPointsCountSystemCreated(PointsCountSystem pointsCountSystem)
        {
            _pointsCountSystem = pointsCountSystem;
            OnPointsChanged(_pointsCountSystem.Points.Value);
            _pointsCountSystem.Points.Changed += OnPointsChanged;
        }
        
        private void OnGameStarted()
        {
            _view.SetActive(true);
        }
        
        private void OnGameStopped()
        {
            if (!_game.IsDestroyed)
                _view.SetActive(false);
            
            _playerControlSystem.GameModelCreated -= OnPlayerCreated;
            _pointsCountSystem.Points.Changed -= OnPointsChanged;
            _player.Position.Changed -= OnPlayerPositionChanged;
            _player.Rotation.Changed -= OnPlayerRotationChanged;
            _player.Velocity.Changed -= OnPlayerVelocityChanged;
            _player.LaserCharges.Changed -= OnPlayerLaserChargesChanged;
            _player.LaserReloadTime.Changed -= OnPlayerLaserCooldownChanged;
        }

        private void OnPlayerCreated(IGameModel gameModel)
        {
            if (gameModel is not Player player)
                return;

            _player = player;
            
            var position = _player.Position;
            var rotation = _player.Rotation;
            var velocity = _player.Velocity;
            var laserCharges = _player.LaserCharges;
            var laserReloadTime = _player.LaserReloadTime;
            OnPlayerPositionChanged(position.Value);
            OnPlayerRotationChanged(rotation.Value);
            OnPlayerVelocityChanged(velocity.Value);
            OnPlayerLaserChargesChanged(laserCharges.Value);
            OnPlayerLaserCooldownChanged(laserReloadTime.Value);
            position.Changed += OnPlayerPositionChanged;
            rotation.Changed += OnPlayerRotationChanged;
            velocity.Changed += OnPlayerVelocityChanged;
            laserCharges.Changed += OnPlayerLaserChargesChanged;
            laserReloadTime.Changed += OnPlayerLaserCooldownChanged;
        }

        private void OnPlayerPositionChanged(Vector3 playerPosition)
        {
            var x = Math.Round(playerPosition.x, 1, MidpointRounding.AwayFromZero);
            var z = Math.Round(playerPosition.z, 1, MidpointRounding.AwayFromZero);
            _view.PositionLabel.text = $"Position: ({x}; {z})";
        }
        
        private void OnPlayerRotationChanged(Quaternion playerRotation)
        {
            var roundedRotation = Math.Round(playerRotation.eulerAngles.y);
            _view.RotationLabel.text = $"Rotation: {roundedRotation}";
        }
        
        private void OnPlayerVelocityChanged(float playerVelocity)
        {
            var roundedVelocity = Math.Round(playerVelocity);
            _view.VelocityLabel.text = $"Velocity: {roundedVelocity}";
        }
        
        private void OnPlayerLaserChargesChanged(int charges)
        {
            _view.LaserChargesLabel.text = $"Laser charges: {charges}";
        }
        
        private void OnPlayerLaserCooldownChanged(float cooldown)
        {
            var roundedCooldown = Math.Round(cooldown);
            _view.LaserCooldownLabel.text = $"Laser cooldown: {roundedCooldown}";
        }
        
        private void OnPointsChanged(int points)
        {
            _view.PointsLabel.text = $"Points: {points}";
        }
    }
}