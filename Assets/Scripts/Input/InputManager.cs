using System;
using TNRD;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        public float HorizontalDirection { get; private set; }

        [SerializeField] private GameObject _character;
        [SerializeField] private SerializableInterface<ICharacterFireRequest> characterController;

        private IMoveComponent _moveCharacter;
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

        private void Start()
        {
            _moveCharacter = _character.GetComponent<IMoveComponent>();
        }

        private void Update()
        {
            HorizontalDirection = _inputActions.BaseMap.PlayerMove.ReadValue<Single>();
        }

        private void FixedUpdate()
        {
            this._moveCharacter.MoveByRigidbodyVelocity(new Vector2(this.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }

        private void OnFire(InputAction.CallbackContext context)
        {
            characterController.Value?.FireRequest();
        }
    }
}
