using System;
using TNRD;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour, IEnemyMoveAgent
    {
        [SerializeField] private SerializableInterface<IMoveComponent> _moveComponent;
        [SerializeField] private Single _arrivalDistanceTreshold = 0.25f;
        
        private Boolean _isReached;
        private Vector2 _destinationPoint;

        public void SetDestination(Vector2 endPoint)
        {
            _destinationPoint = endPoint;
            _isReached = false;
        }

        public Boolean IsReached()
        {
            return _isReached;
        }

        private void FixedUpdate()
        {
            if (_isReached)
            {
                return;
            }
            
            var vector = _destinationPoint - (Vector2) transform.position;

            if (vector.magnitude <= _arrivalDistanceTreshold)
            {
                _isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            _moveComponent.Value.MoveByRigidbodyVelocity(direction);
        }
    }
}