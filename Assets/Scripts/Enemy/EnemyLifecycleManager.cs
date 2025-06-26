using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TNRD;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyLifecycleManager : MonoBehaviour
    {
        [SerializeField]
        private SerializableInterface<IEnemyPool> _enemyPool;
        
        [SerializeField]
        private SerializableInterface<IBulletLaucnher> _bulletSystem;
        
        private readonly HashSet<GameObject> m_activeEnemies = new();

        [SerializeField] private BulletConfig _bulletConfig;

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                var enemy = this._enemyPool.Value.TryGetNewEnemy();
                if (enemy != null)
                {
                    if (this.m_activeEnemies.Add(enemy))
                    {
                        EnemyComponentProvider tempEnemy = enemy.GetComponent<EnemyComponentProvider>();

                        if (tempEnemy != null)
                        {
                            tempEnemy.HitPointInstance.HpEmpty += this.OnDestroyed;
                            tempEnemy.EnemyAttackAgentInstance.OnFire += this.OnFire;
                        }
                    }    
                }
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (m_activeEnemies.Remove(enemy))
            {
                EnemyComponentProvider tempEnemy = enemy.GetComponent<EnemyComponentProvider>();

                if (tempEnemy != null)
                {
                    tempEnemy.HitPointInstance.HpEmpty -= this.OnDestroyed;
                    tempEnemy.EnemyAttackAgentInstance.OnFire -= this.OnFire;
                }

                _enemyPool.Value.RemoveEnemy(enemy);
            }
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction, IWeaponComponent weaponComponent)
        {
            _bulletSystem.Value.FlyBulletByArgs(new BulletData
            {
                isPlayer = false,
                physicsLayer = this._bulletConfig.physicsLayer,
                color = this._bulletConfig.color,
                damage = this._bulletConfig.damage,
                position = position,
                velocity = direction * this._bulletConfig.speed
            });
        }
    }
}