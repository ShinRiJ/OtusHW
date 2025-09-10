using Zenject;
using UnityEngine;
using System;

namespace ShootEmUp
{
    public class EnemySystemInstaller : MonoInstaller
    {
        [SerializeField] private Single _spawnPeriod;
        [SerializeField] private Int32 _enemyMaxCount;
        [SerializeField] private GameObject _enemyPrefab;

        [SerializeField] private BulletConfig _enemyBulletConfig;
        
        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private Transform[] _attackPositions;

        public override void InstallBindings()
        {
            //--------------------------В целом ничего интересного--------------------------
            Container.BindInterfacesAndSelfTo<EnemyPositions>().AsSingle().WithArguments(_spawnPositions, _attackPositions).NonLazy();

            Container.BindMemoryPool<EnemyFacade, EnemyPool>()
                .WithInitialSize(10)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(_enemyPrefab)
                .UnderTransformGroup("Enemies");

            Container.BindInterfacesAndSelfTo<EnemyLifecycleManager>().AsSingle().WithArguments(_enemyBulletConfig, _spawnPeriod, _enemyMaxCount).NonLazy();

            //--------------------------Тикер--------------------------
            Container.BindInterfacesAndSelfTo<GameTickableController>().AsSingle().NonLazy();
        }
    }
}
