using System;
using TNRD;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterFireController : MonoBehaviour, ICharacterFireRequest
    {
        [SerializeField] private SerializableInterface<IWeaponComponent> _weaponComponent;
        [SerializeField] private SerializableInterface<IBulletLaucnher> _bulletSystem;
        [SerializeField] private SerializableInterface<IInputManager> _inputManager;

        [SerializeField] private BulletConfig _bulletConfig;

        private Boolean _fireRequired;

        private void Start()
        {
            _inputManager.Value.OnFireAction += FireRequest;
        }

        private void FixedUpdate()
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
                PhysicsLayer = _bulletConfig.physicsLayer,
                Color = _bulletConfig.color,
                Damage = _bulletConfig.damage,
                Position = _weaponComponent.Value.GetShootingPosition(),
                Velocity = _weaponComponent.Value.GetShootingVelocity(Vector3.up, _bulletConfig.speed)
            });
        }

        public void FireRequest()
        {
            _fireRequired = true;
        }
    }
}
