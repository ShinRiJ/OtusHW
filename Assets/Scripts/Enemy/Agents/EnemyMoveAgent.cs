using System;
using TNRD;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : IEnemyMoveAgent, IFixedTickableCustom
    {
        [InjectLocal] private IMoveComponent _moveComponent;

        private Transform _myTransform;
        private Single _arrivalDistanceTreshold = 0.25f;
        
        private Boolean _isReached;
        private Vector2 _destinationPoint;

        public EnemyMoveAgent(Transform transform)
        {
            _myTransform = transform;
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destinationPoint = endPoint;
            _isReached = false;
        }

        public Boolean IsReached()
        {
            return _isReached;
        }

        public void FixedTick()
        {
            if (_isReached)
            {
                return;
            }
            
            var vector = _destinationPoint - (Vector2)_myTransform.position;

            if (vector.magnitude <= _arrivalDistanceTreshold)
            {
                _isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            _moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}