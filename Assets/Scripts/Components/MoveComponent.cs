using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class MoveComponent : IMoveComponent
    {
        private Rigidbody2D _rigidbody2D;
        private Single _speed = 5.0f;

        public MoveComponent(Rigidbody2D rigidbody2D, Single speed)
        {
            _rigidbody2D = rigidbody2D;
            _speed = speed;
        }

        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            var nextPosition = _rigidbody2D.position + vector * _speed;
            _rigidbody2D.MovePosition(nextPosition);
        }
    }
}