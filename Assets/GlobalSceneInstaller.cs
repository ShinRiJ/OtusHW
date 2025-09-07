using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class GlobalSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private GameObject _playerPrefab;

        [SerializeField] private Transform _playerRoot;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _backgroundTransform;

        [SerializeField] private Int32 _initialPoolSize = 75;

        [SerializeField] private BorderTransforms _borderTransforms;
        [SerializeField] private BackGroundParams _backGroundParams;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<StartGameSignal>();
            Container.DeclareSignal<FinishGameSignal>();
            Container.DeclareSignal<ResumeGameSignal>();
            Container.DeclareSignal<PauseGameSignal>();

            Container.Bind<Transform>().WithId("PlayerSpawnPoint").FromInstance(_playerSpawnPoint);

            Container.Bind<ICharacterDeathNotifier>().
                FromSubContainerResolve().
                ByNewContextPrefab(_playerPrefab).
                UnderTransform(_playerRoot).
                AsSingle();

            Container.BindInterfacesAndSelfTo<LevelBounds>().FromInstance(new LevelBounds(_borderTransforms)).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelBackground>().FromInstance(new LevelBackground(_backGroundParams, _backgroundTransform)).AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<DeathPlayerObserver>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<GameStateManager>().AsSingle().NonLazy();

            Container.BindMemoryPool<Bullet, BulletPool>()
                .WithInitialSize(_initialPoolSize)
                .FromComponentInNewPrefab(_bulletPrefab)
                .UnderTransformGroup("Bullets");

            Container.BindInterfacesTo<BulletSystem>()
                .AsSingle()
                .NonLazy();


            Container.BindInterfacesAndSelfTo<GameTickableController>().AsSingle().NonLazy();
        }
    }

    public class StartGameSignal { }
    public class PauseGameSignal { }
    public class FinishGameSignal { }
    public class ResumeGameSignal { }


}
