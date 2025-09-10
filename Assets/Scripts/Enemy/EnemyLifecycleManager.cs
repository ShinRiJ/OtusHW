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
    public sealed class EnemyLifecycleManager : IInitializable, IDisposable
    {
        [Inject] private SignalBus _signulBusl; 
        [Inject] private IBulletLaucnher _bulletSystem;

        [InjectLocal] private EnemyPool _enemyPool;

        [Inject(Id = "Player")] GameObject _target;

        private BulletConfig _bulletConfig;
        private Single _cyclePeriod = 1;
        
        private readonly HashSet<EnemyFacade> _activeEnemies = new();
        private CancellationTokenSource _cts;
        private Boolean _isPaused = false;
        private Int32 _enemyMaxCount;

        public EnemyLifecycleManager(BulletConfig bulletConfig, Single cyclePriod, Int32 enemyMaxCount)
        {
            _bulletConfig = bulletConfig;
            _cyclePeriod = cyclePriod;
            _enemyMaxCount = enemyMaxCount;
        }

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
                await UniTask.Delay(TimeSpan.FromSeconds(_cyclePeriod), cancellationToken: token);
                await UniTask.WaitUntil(() => !_isPaused);

                if(_activeEnemies.Count < _enemyMaxCount)
                {
                    EnemyFacade enemy = _enemyPool.Spawn(_target);
                    enemy.OnDeath += OnDestroyed;
                    enemy.EnemyAttackAgent.OnFire += OnFire;

                    _activeEnemies.Add(enemy);
                }
            }
        }

        private void ClearEnemies()
        {
            if(_activeEnemies.Count == 0)
            {
                return;
            }

            foreach (EnemyFacade enemy in _activeEnemies)
            {
                _enemyPool.Despawn(enemy);
            }

            _activeEnemies.Clear();
        }

        private void OnDestroyed(EnemyFacade enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                 enemy.OnDeath -= OnDestroyed;
                enemy.EnemyAttackAgent.OnFire -= OnFire;

                _enemyPool.Despawn(enemy);
            }
        }


        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction, IWeaponComponent weaponComponent)
        {
            _bulletSystem.FlyBulletByArgs(new BulletData
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