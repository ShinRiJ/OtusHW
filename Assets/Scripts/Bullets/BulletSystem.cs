using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace ShootEmUp
{
    public interface IBulletLaucnher
    {
        public void FlyBulletByArgs(BulletData args);
    }

    public sealed class BulletSystem : MonoBehaviour, IBulletLaucnher
    {
        [SerializeField]
        private int initialCount = 75;
        
        [SerializeField] private Transform _poolBulletContainer;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private SerializableInterface<ILevelBoundCheck> _levelBoundsChecker;

        private readonly Queue<Bullet> m_bulletPool = new();
        private readonly HashSet<Bullet> m_activeBullets = new();
        private readonly List<Bullet> m_activeBulletsFrameCache = new();
        
        private void Awake()
        {
            for (var i = 0; i < this.initialCount; i++)
            {
                var bullet = Instantiate(this._bulletPrefab, this._poolBulletContainer);
                this.m_bulletPool.Enqueue(bullet);
            }
        }
        
        private void FixedUpdate()
        {
            this.m_activeBulletsFrameCache.Clear();
            this.m_activeBulletsFrameCache.AddRange(this.m_activeBullets);

            for (int i = 0, count = this.m_activeBulletsFrameCache.Count; i < count; i++)
            {
                var bullet = this.m_activeBulletsFrameCache[i];

                if (!this._levelBoundsChecker.Value.InBounds(bullet.transform.position))
                    this.BulletEndLife(bullet);
            }
        }

        public void FlyBulletByArgs(BulletData args)
        {
            if (this.m_bulletPool.TryDequeue(out var bullet))
                bullet.transform.SetParent(this._worldTransform);
            else
            {
                bullet = Instantiate(this._bulletPrefab, this._worldTransform);
                Debug.LogWarning("Bullet pull empty!");
            }

            bullet.BulletSetup(args);

            if (this.m_activeBullets.Add(bullet))
                bullet.OnCollisionEntered += this.OnBulletCollision;
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
             BulletUtils.TryDealDamage(bullet, collision.gameObject);
            this.BulletEndLife(bullet);
        }

        private void BulletEndLife(Bullet bullet)
        {
            if (this.m_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= this.OnBulletCollision;
                bullet.transform.SetParent(this._poolBulletContainer);
                this.m_bulletPool.Enqueue(bullet);
            }
        }
    }
}