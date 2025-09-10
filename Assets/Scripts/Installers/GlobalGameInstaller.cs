using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class GlobalGameInstaller : MonoInstaller
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
            //-------------------Сигнальная шина-------------------
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<StartGameSignal>();
            Container.DeclareSignal<FinishGameSignal>();
            Container.DeclareSignal<ResumeGameSignal>();
            Container.DeclareSignal<PauseGameSignal>();

            //-------------------Системы игры в нужном порядке-------------------
            Container.Bind<Transform>().WithId("PlayerSpawnPoint").FromInstance(_playerSpawnPoint);

            Container.BindInterfacesAndSelfTo<LevelBounds>().FromInstance(new LevelBounds(_borderTransforms)).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelBackground>().FromInstance(new LevelBackground(_backGroundParams, _backgroundTransform)).AsSingle().NonLazy();

            Container.Bind<PlayerFacade>().FromComponentInNewPrefab(_playerPrefab).UnderTransform(_playerRoot).AsSingle();

            Container.Bind<GameObject>().WithId("Player").FromResolveGetter<PlayerFacade>(x => x.gameObject).AsSingle(); //Тащим GO игрока для системы спавна врагов
            Container.Bind<ICharacterDeathNotifier>().FromResolveGetter<PlayerFacade>(x => x.CharacterDeathNotifier).AsSingle(); //Тащим интерфейс с собтием смерти игрока для рестрта

            Container.BindInterfacesAndSelfTo<DeathPlayerObserver>().AsSingle().NonLazy(); 

            Container.BindInterfacesAndSelfTo<GameStateManager>().AsSingle().NonLazy(); 

            Container.BindMemoryPool<Bullet, BulletPool>().WithInitialSize(_initialPoolSize).FromComponentInNewPrefab(_bulletPrefab).UnderTransformGroup("Bullets");

            Container.BindInterfacesTo<BulletSystem>().AsSingle().NonLazy();

            //-------------------Тикер-------------------
            Container.BindInterfacesAndSelfTo<GameTickableController>().AsSingle().NonLazy();
        }
    }
}
