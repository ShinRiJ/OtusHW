using System;
using TNRD;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour, IStartGameListener, IFinishGameListener, IInputManager, ICommonTickable
    {
        public float MoveDirection { get; private set; }
        public event Action OnFireAction;

        private PlayerInputActions _inputActions;

        private void Awake()
        {
            _inputActions = new PlayerInputActions();
        }

        public void StartGame()
        {
            _inputActions.Enable();
            _inputActions.BaseMap.Fire.performed += OnFire;
        }

        public void FinishGame()
        {
            _inputActions.BaseMap.Fire.performed -= OnFire;
            _inputActions.Disable();
        }

        public void Tick()
        {
            MoveDirection = _inputActions.BaseMap.PlayerMove.ReadValue<Single>();
        }

        private void OnFire(InputAction.CallbackContext context)
        {
            OnFireAction?.Invoke();
        }
    }
}
