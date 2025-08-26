using System;
using System.Collections.Generic;
using System.Linq;
using TNRD;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour, IBulletLaucnher, IFixedTickable, IStartGameListener
    {
        [SerializeField]
        private Int32 _initialCount = 75;
        
        [SerializeField] private Transform _poolBulletContainer;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private SerializableInterface<ILevelBoundCheck> _levelBoundsChecker;
        [SerializeField] private BulletStateInstaller _bulletStateInstaller;

        private readonly Queue<Bullet> _bulletPool = new();
        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _activeBulletsFrameCache = new();
        
        private void Awake()
        {
            for (var i = 0; i < _initialCount; i++)
            {
                var bullet = Instantiate(_bulletPrefab, _poolBulletContainer);
                _bulletPool.Enqueue(bullet);
            }
        }
        
        public void FixedTick()
        {
            _activeBulletsFrameCache.Clear();
            _activeBulletsFrameCache.AddRange(_activeBullets);

            for (int i = 0, count = _activeBulletsFrameCache.Count; i < count; i++)
            {
                var bullet = _activeBulletsFrameCache[i];

                if (!_levelBoundsChecker.Value.InBounds(bullet.transform.position))
                {
                    BulletEndLife(bullet);
                }
            }
        }

        public void FlyBulletByArgs(BulletData args)
        {
            if (_bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(_worldTransform);
            }
            else
            {
                bullet = Instantiate(_bulletPrefab, _worldTransform);
                Debug.LogWarning("Bullet pull empty!");
            }

            bullet.BulletSetup(args);

            if (_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += OnBulletCollision;
            }

            _bulletStateInstaller.RegisterBuletStateHandles(bullet.GetComponents<IGameStateListener>());
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.TryDealDamage(bullet, collision.gameObject);
            BulletEndLife(bullet);
        }

        private void BulletEndLife(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                bullet.transform.SetParent(_poolBulletContainer);
                _bulletPool.Enqueue(bullet);

                _bulletStateInstaller.DeleteBuletStateHandles(bullet.GetComponents<IGameStateListener>());
            }
        }

        public void StartGame()
        {
            foreach (var bullet in _activeBullets.ToList())
                BulletEndLife(bullet);
        }
    }
}