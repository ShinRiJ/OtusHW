using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        public Boolean IsPlayer { get; private set; }
        public Int32 Damage { get; private set; }

        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }

        private void SetVelocity(Vector2 velocity)
        {
            _rigidbody2D.velocity = velocity;
        }

        private void SetPhysicsLayer(PhysicsLayer physicsLayer)
        {
            gameObject.layer = (Int32) physicsLayer;
        }

        private void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        private void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        public void BulletSetup(BulletData bulletData)
        {
            SetPosition(bulletData.Position);
            SetColor(bulletData.Color);
            SetPhysicsLayer(bulletData.PhysicsLayer);
            Damage = bulletData.Damage;
            IsPlayer = bulletData.IsPlayer;
            SetVelocity(bulletData.Velocity);
        }
    }
}