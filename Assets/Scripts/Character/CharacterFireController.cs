using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class CharacterFireController : ICharacterFireRequest, IInitializable, IDisposable, IFixedTickableCustom
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private IBulletLaucnher _bulletSystem;

        [InjectLocal] private IWeaponComponent _weaponComponent;
        [InjectLocal] private IInputManager _inputManager;

        private BulletConfig _bulletConfig;

        private Boolean _fireRequired;

        public void Initialize()
        {
            _signalBus.Subscribe<StartGameSignal>(OnStartGame);
            _signalBus.Subscribe<FinishGameSignal>(OnFinishGame);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<StartGameSignal>(OnStartGame);
            _signalBus.Unsubscribe<FinishGameSignal>(OnFinishGame);
        }

        public CharacterFireController(BulletConfig bulletConfig)
        {
            _bulletConfig = bulletConfig;
        }

        public void OnStartGame()
        {
            _inputManager.OnFireAction += FireRequest;
        }

        public void OnFinishGame()
        {
            _inputManager.OnFireAction -= FireRequest;
        }

        public void FixedTick()
        {
            TryFire();
        }

        private void TryFire()
        {
            if (_fireRequired == false)
            {
                return;
            }

            _fireRequired = false;
            OnFlyBullet();
        }

        private void OnFlyBullet()
        {
            _bulletSystem.FlyBulletByArgs(new BulletData
            {
                IsPlayer = true,
                PhysicsLayer = _bulletConfig.PhysicsLayer,
                Color = _bulletConfig.Color,
                Damage = _bulletConfig.Damage,
                Position = _weaponComponent.GetShootingPosition(),
                Velocity = _weaponComponent.GetShootingVelocity(Vector3.up, _bulletConfig.Speed)
            });
        }

        public void FireRequest()
        {
            _fireRequired = true;
        }
    }
}
