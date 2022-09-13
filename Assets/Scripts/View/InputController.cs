using System;
using Model;
using UnityEngine;
using UnityEngine.InputSystem;

namespace View
{
    public class InputController : MonoBehaviour, PlayerInputActions.IDefaultActions
    {
        [SerializeField] private PlayerPresenter _presenter;
        private PlayerInputActions _controls;

        public void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new PlayerInputActions();
                _controls.Default.SetCallbacks(this);
            }

            _controls.Default.Enable();
        }

        public void OnDisable()
        {
            _controls.Default.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (Game.Instance.IsStopped)
                return;
            
            var value = context.ReadValueAsButton();
            _presenter.Player.GivenAcceleration = Convert.ToInt32(value);
        }

        public void OnRotate(InputAction.CallbackContext context)
        {
            if (Game.Instance.IsStopped)
                return;
            
            var value = context.ReadValue<float>();
            _presenter.Player.GivenRotation = value;
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (Game.Instance.IsStopped)
                return;
            
            var value = context.ReadValueAsButton();
            if (context.performed && value)
                _presenter.Player.Fire();
        }

        public void OnAlternativeFire(InputAction.CallbackContext context)
        {
            if (Game.Instance.IsStopped)
                return;
            
            var value = context.ReadValueAsButton();
            if (context.performed && value)
                _presenter.AlternativeFire();
        }
    }
}