using System;
using UnityEngine;

namespace ShootEmUp
{
    public struct BulletData
    {
        public Vector2 position;
        public Vector2 velocity;
        public Color color;
        public PhysicsLayer physicsLayer;
        public int damage;
        public bool isPlayer;
    }

    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [NonSerialized] public Boolean _isPlayer;
        [NonSerialized] public int _damage;

        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            this.OnCollisionEntered?.Invoke(this, collision);
        }

        private void SetVelocity(Vector2 velocity)
        {
            this._rigidbody2D.velocity = velocity;
        }

        private void SetPhysicsLayer(PhysicsLayer physicsLayer)
        {
            this.gameObject.layer = (Int32) physicsLayer;
        }

        private void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }

        private void SetColor(Color color)
        {
            this._spriteRenderer.color = color;
        }

        public void BulletSetup(BulletData bulletData)
        {
            SetPosition(bulletData.position);
            SetColor(bulletData.color);
            SetPhysicsLayer(bulletData.physicsLayer);
            _damage = bulletData.damage;
            _isPlayer = bulletData.isPlayer;
            SetVelocity(bulletData.velocity);
        }
    }
}