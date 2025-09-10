using ShootEmUp;
using System;
using UnityEngine;
using Zenject;

public class EnemyUnitInstaller : MonoInstaller
{
    [SerializeField] private Boolean _isPlayer;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Int32 _enemyHitPoints;
    [SerializeField] private Single _enemySpeed;

    [SerializeField] private Rigidbody2D _rigidbody2D;
    override public void InstallBindings()
    {
        // --- Локальные компоненты каждого врага ---
        Container.BindInterfacesAndSelfTo<HitPointsComponent>().AsSingle().WithArguments(_enemyHitPoints, gameObject).NonLazy();
        Container.BindInterfacesAndSelfTo<WeaponComponent>().AsSingle().WithArguments(_firePoint);
        Container.BindInterfacesAndSelfTo<MoveComponent>().AsSingle().WithArguments(_rigidbody2D, _enemySpeed);
        Container.BindInterfacesAndSelfTo<TeamComponent>().AsSingle().WithArguments(_isPlayer);
        Container.BindInterfacesAndSelfTo<EnemyMoveAgent>().AsSingle().WithArguments(transform).NonLazy();
        Container.BindInterfacesAndSelfTo<EnemyAttackAgent>().AsSingle().WithArguments(gameObject).NonLazy();

        //Container.BindInterfacesAndSelfTo<EnemyFacade>().AsSingle().NonLazy();


        // --- Тикер ---
        Container.BindInterfacesAndSelfTo<GameTickableController>().AsSingle().NonLazy();
    }
}
