using TNRD;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ShootEmUp
{
    public sealed class CharacterMoveController : MonoBehaviour, IFixedTickable
    {
        [SerializeField] private SerializableInterface<IInputManager> _inputManager;
        [SerializeField] private SerializableInterface<IMoveComponent> _moveComponent;

        public void FixedTick()
        {
            _moveComponent.Value.MoveByRigidbodyVelocity(
                new Vector2(_inputManager.Value.MoveDirection, 0) * Time.fixedDeltaTime
            );
        }
    }
}
