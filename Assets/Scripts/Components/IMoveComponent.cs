using UnityEngine;

namespace ShootEmUp
{
    public interface IMoveComponent
    {
        public void MoveByRigidbodyVelocity(Vector2 vector);
    }
}