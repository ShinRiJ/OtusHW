using TNRD;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ShootEmUp
{
    public sealed class CharacterMoveController : IFixedTickableCustom
    {
        private IInputManager _inputManager;
        private IMoveComponent _moveComponent;

        public CharacterMoveController(IInputManager inputManager, IMoveComponent moveComponent)
        {
            _inputManager = inputManager;
            _moveComponent = moveComponent;
        }

        public void FixedTick()
        {
            _moveComponent.MoveByRigidbodyVelocity(
                new Vector2(_inputManager.MoveDirection, 0) * Time.fixedDeltaTime
            );
        }
    }
}
