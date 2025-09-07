using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TNRD;
using UnityEngine;
using Zenject;
namespace ShootEmUp
{
    public sealed class EnemyLifecycleManager : MonoBehaviour, IInitializable, IDisposable
    {
        [Inject] private SignalBus _signulBusl; 

        [SerializeField] private SerializableInterface<IEnemyPool> _enemyPool;
        [SerializeField] private SerializableInterface<IBulletLaucnher> _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private Single _cyclePeriod = 1;
        
        private readonly HashSet<GameObject> _activeEnemies = new();
        private CancellationTokenSource _cts;
        private Boolean _isPaused = false;

        public void Initialize()
        {
            _signulBusl.Subscribe<StartGameSignal>(OnStartGame);
            _signulBusl.Subscribe<FinishGameSignal>(OnFinishGame);
            _signulBusl.Subscribe<ResumeGameSignal>(OnResumeGame);
            _signulBusl.Subscribe<PauseGameSignal>(OnPauseGame);

        }

        public void Dispose()
        {
            _signulBusl.Unsubscribe<StartGameSignal>(OnStartGame);
            _signulBusl.Unsubscribe<FinishGameSignal>(OnFinishGame);
            _signulBusl.Unsubscribe<ResumeGameSignal>(OnResumeGame);
            _signulBusl.Unsubscribe<PauseGameSignal>(OnPauseGame);
        }

        public void OnStartGame()
        {
            if(_isPaused)
            {
                _isPaused = false;
            }

            _cts = new CancellationTokenSource();
            SpawnCycle(_cts.Token).Forget();

            ClearEnemies();
        }

        public void OnFinishGame()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private async UniTaskVoid SpawnCycle(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await UniTask.WaitUntil(() => !_isPaused);
                await UniTask.Delay(TimeSpan.FromSeconds(_cyclePeriod), cancellationToken: token);

                if(_isPaused)
                {
                    await UniTask.WaitUntil(() => !_isPaused);
                    await UniTask.Delay(TimeSpan.FromSeconds(_cyclePeriod), cancellationToken: token);
                }

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

        private void ClearEnemies()
        {
            if(_activeEnemies.Count == 0)
            {
                return;
            }

            foreach (var enemy in _activeEnemies)
            {
                if (enemy == null) continue;

                RemoveEnemyFromGame(enemy);

                _enemyPool.Value.RemoveEnemy(enemy);
            }

            _activeEnemies.Clear();
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                RemoveEnemyFromGame(enemy);
                _enemyPool.Value.RemoveEnemy(enemy);
            }
        }

        private void RemoveEnemyFromGame(GameObject enemy)
        {
            EnemyComponentProvider tempEnemy = enemy.GetComponent<EnemyComponentProvider>();

            if (tempEnemy != null)
            {
                tempEnemy.HitPointInstance.OnHPEmpty -= OnDestroyed;
                tempEnemy.EnemyAttackAgentInstance.OnFire -= OnFire;
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

        public void OnResumeGame()
        {
            _isPaused = false;
        }

        public void OnPauseGame()
        {
            _isPaused = true;
        }


    }
}