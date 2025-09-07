using System;
using System.Collections.Generic;
using System.Linq;
using TNRD;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletSystem : IBulletLaucnher, IFixedTickableCustom, IInitializable, IDisposable
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private BulletPool _bulletPool;
        [Inject] private ILevelBoundCheck _levelBoundsChecker;

        private HashSet<Bullet> _activeBullets;
        private List<Bullet> _activeBulletsFrameCache;

        public void Initialize()
        {
            _activeBullets = new();
            _activeBulletsFrameCache = new();

            _signalBus.Subscribe<StartGameSignal>(OnStartGame);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<StartGameSignal>(OnStartGame);
        }

        public void FixedTick()
        {
            _activeBulletsFrameCache.Clear();
            _activeBulletsFrameCache.AddRange(_activeBullets);

            for (int i = 0, count = _activeBulletsFrameCache.Count; i < count; i++)
            {
                var bullet = _activeBulletsFrameCache[i];

                if (!_levelBoundsChecker.InBounds(bullet.transform.position))
                {
                    BulletEndLife(bullet);
                }
            }
        }

        public void FlyBulletByArgs(BulletData args)
        {
            Bullet bullet = _bulletPool.Spawn(args);
            bullet.OnCollisionEntered += OnBulletCollision;
            _activeBullets.Add(bullet);
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.TryDealDamage(bullet, collision.gameObject);
            bullet.OnCollisionEntered -= OnBulletCollision;

            BulletEndLife(bullet);
        }

        private void BulletEndLife(Bullet bullet)
        {
            _bulletPool.Despawn(bullet);
            _activeBullets.Remove(bullet);
        }

        public void OnStartGame()
        {
            foreach (var item in _activeBullets)
            {
                BulletEndLife(item);
            }

            _activeBullets = new();
            _activeBulletsFrameCache = new();
        }
    }
}