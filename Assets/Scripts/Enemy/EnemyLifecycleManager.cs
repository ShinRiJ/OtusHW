using System;
using System.Collections;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyLifecycleManager : MonoBehaviour
    {
        [SerializeField] private SerializableInterface<IEnemyPool> _enemyPool;
        [SerializeField] private SerializableInterface<IBulletLaucnher> _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private Single _cyclePeriod = 1;
        
        private readonly HashSet<GameObject> _activeEnemies = new();

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(_cyclePeriod);
                var enemy = _enemyPool.Value.TryGetNewEnemy();
                if (enemy != null)
                {
                    if (_activeEnemies.Add(enemy))
                    {
                        EnemyComponentProvider tempEnemy = enemy.GetComponent<EnemyComponentProvider>();

                        if (tempEnemy != null)
                        {
                            tempEnemy.HitPointInstance.OnHPEmpty += OnDestroyed;
                            tempEnemy.EnemyAttackAgentInstance.OnFire += OnFire;
                        }
                    }    
                }
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                EnemyComponentProvider tempEnemy = enemy.GetComponent<EnemyComponentProvider>();

                if (tempEnemy != null)
                {
                    tempEnemy.HitPointInstance.OnHPEmpty -= OnDestroyed;
                    tempEnemy.EnemyAttackAgentInstance.OnFire -= OnFire;
                }

                _enemyPool.Value.RemoveEnemy(enemy);
            }
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction, IWeaponComponent weaponComponent)
        {
            _bulletSystem.Value.FlyBulletByArgs(new BulletData
            {
                IsPlayer = false,
                PhysicsLayer = _bulletConfig.PhysicsLayer,
                Color = _bulletConfig.Color,
                Damage = _bulletConfig.Damage,
                Position = position,
                Velocity = direction * _bulletConfig.Speed
            });
        }
    }
}