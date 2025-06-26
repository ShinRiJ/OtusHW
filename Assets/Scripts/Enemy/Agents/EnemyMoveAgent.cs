using System;
using TNRD;
using UnityEngine;

namespace ShootEmUp
{
    public interface IEnemyMoveAgent
    {
        public Boolean IsReached();
        public void SetDestination(Vector2 endPoint);
    }
    public sealed class EnemyMoveAgent : MonoBehaviour, IEnemyMoveAgent
    {
        [SerializeField]
        private SerializableInterface<IMoveComponent> moveComponent;
        
        private bool _isReached;

        private Vector2 destination;

        public void SetDestination(Vector2 endPoint)
        {
            this.destination = endPoint;
            this._isReached = false;
        }

        public Boolean IsReached() => this._isReached;

        private void FixedUpdate()
        {
            if (this._isReached)
                return;
            
            var vector = this.destination - (Vector2) this.transform.position;

            if (vector.magnitude <= 0.25f)
            {
                this._isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            this.moveComponent.Value.MoveByRigidbodyVelocity(direction);
        }
    }
}