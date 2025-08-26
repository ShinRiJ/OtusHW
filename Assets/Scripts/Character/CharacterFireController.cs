using System;
using TNRD;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterFireController : MonoBehaviour, ICharacterFireRequest, IStartGameListener, IFinishGameListener, IFixedTickable
    {
        [SerializeField] private SerializableInterface<IWeaponComponent> _weaponComponent;
        [SerializeField] private SerializableInterface<IBulletLaucnher> _bulletSystem;
        [SerializeField] private SerializableInterface<IInputManager> _inputManager;

        [SerializeField] private BulletConfig _bulletConfig;

        private Boolean _fireRequired;

        public void StartGame()
        {
            _inputManager.Value.OnFireAction += FireRequest;
        }

        public void FinishGame()
        {
            _inputManager.Value.OnFireAction -= FireRequest;
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
            _bulletSystem.Value.FlyBulletByArgs(new BulletData
            {
                IsPlayer = true,
                PhysicsLayer = _bulletConfig.PhysicsLayer,
                Color = _bulletConfig.Color,
                Damage = _bulletConfig.Damage,
                Position = _weaponComponent.Value.GetShootingPosition(),
                Velocity = _weaponComponent.Value.GetShootingVelocity(Vector3.up, _bulletConfig.Speed)
            });
        }

        public void FireRequest()
        {
            _fireRequired = true;
        }

    }
}
