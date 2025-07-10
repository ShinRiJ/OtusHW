using System;
using TNRD;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour, IInputManager
    {
        public float MoveDirection { get; private set; }
        public event Action OnFireAction;

        private PlayerInputActions _inputActions;

        private void Awake()
        {
            _inputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.BaseMap.Fire.performed += OnFire;
        }

        private void OnDisable()
        {
            _inputActions.BaseMap.Fire.performed -= OnFire;
            _inputActions.Disable();
        }

        private void Update()
        {
            MoveDirection = _inputActions.BaseMap.PlayerMove.ReadValue<Single>();
        }

        private void OnFire(InputAction.CallbackContext context)
        {
            OnFireAction?.Invoke();
        }
    }
}
