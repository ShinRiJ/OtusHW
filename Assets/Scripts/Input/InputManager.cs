using System;
using TNRD;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace ShootEmUp
{
    public sealed class InputManager : IInputManager, ICommonTickableCustom, IInitializable, IDisposable
    {
        [Inject] private SignalBus _signalBus;

        public float MoveDirection { get; private set; }
        public event Action OnFireAction;

        private PlayerInputActions _inputActions;

        public void Initialize()
        {
            _inputActions = new();

            _signalBus.Subscribe<StartGameSignal>(OnStartGame);
            _signalBus.Subscribe<FinishGameSignal>(OnFinishGame);
        }

        public void Dispose()
        {

            _signalBus.Unsubscribe<StartGameSignal>(OnStartGame);
            _signalBus.Unsubscribe<FinishGameSignal>(OnFinishGame);
        }

        public void OnStartGame()
        {
            _inputActions.Enable();
            _inputActions.BaseMap.Fire.performed += OnFire;
        }

        public void OnFinishGame()
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
