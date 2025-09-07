using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private Boolean _isPlayer;
        [SerializeField] private Int32 _healthPoint;
        [SerializeField] private Single _moveSpeed;

        [SerializeField] private Transform _firePoint;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        [SerializeField] private BulletConfig _bulletConfig;

        public override void InstallBindings()
        {
            // --- Компоненты персонажа ---
            Container.BindInterfacesAndSelfTo<HitPointsComponent>().AsSingle().WithArguments(_healthPoint, gameObject); ;
            Container.BindInterfacesAndSelfTo<WeaponComponent>().AsSingle().WithArguments(_firePoint);
            Container.BindInterfacesAndSelfTo<TeamComponent>().AsSingle().WithArguments(_isPlayer);
            Container.BindInterfacesAndSelfTo<MoveComponent>().AsSingle().WithArguments(_rigidbody2D, _moveSpeed);

            Container.BindInterfacesAndSelfTo<SpawnComponent>().AsSingle().WithArguments(transform);


            // --- Контроллеры ---
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CharacterStateController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CharacterMoveController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CharacterFireController>().AsSingle().WithArguments(_bulletConfig).NonLazy();

            // --- Тикер ---
            Container.BindInterfacesAndSelfTo<GameTickableController>().AsSingle().NonLazy();
        }
    }
}
