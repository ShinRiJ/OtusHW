using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        [Inject] SignalBus _signalBus;
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        public Boolean IsPlayer { get; private set; }
        public Int32 Damage { get; private set; }

        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private Vector2 _savedVelocity;

        public Bullet(BulletData bulletData)
        {
            Init(bulletData);
        }

        public void Init(BulletData bulletData)
        {
            Initialize();
            Damage = bulletData.Damage;
            IsPlayer = bulletData.IsPlayer;
            SetPosition(bulletData.Position);
            SetColor(bulletData.Color);
            SetPhysicsLayer(bulletData.PhysicsLayer);
            SetVelocity(bulletData.Velocity);
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ResumeGameSignal>(OnResumeGame);
            _signalBus.Subscribe<PauseGameSignal>(OnPauseGame);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ResumeGameSignal>(OnResumeGame);
            _signalBus.Unsubscribe<PauseGameSignal>(OnPauseGame);
        }

        public void Despawned()
        {
            Dispose();
            _rigidbody2D.velocity = Vector2.zero;
            gameObject.SetActive(false);
            OnCollisionEntered = null;
        }


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

        public void OnResumeGame()
        {
            _rigidbody2D.velocity = _savedVelocity;
        }

        public void OnPauseGame()
        {
            _savedVelocity = _rigidbody2D.velocity;
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    public class BulletPool : MonoMemoryPool<BulletData, Bullet>
    {
        protected override void Reinitialize(BulletData data, Bullet bullet)
        {
            bullet.Init(data);
            bullet.gameObject.SetActive(true);
            Container.Inject(bullet);
        }

        protected override void OnDespawned(Bullet bullet)
        {
            bullet.Despawned();
        }
    }


}