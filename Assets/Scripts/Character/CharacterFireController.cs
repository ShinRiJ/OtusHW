using System;
using TNRD;
using UnityEngine;

namespace ShootEmUp
{
    public interface ICharacterFireRequest
    {
        public void FireRequest();
    }

    public class CharacterFireController : MonoBehaviour, ICharacterFireRequest
    {
        [SerializeField] private SerializableInterface<IWeaponComponent> _weaponComponent;
        [SerializeField] private SerializableInterface<IBulletLaucnher> _bulletSystem;

        [SerializeField] private BulletConfig _bulletConfig;

        private Boolean _fireRequired;

        private void FixedUpdate() => this.TryFire();
        private void TryFire()
        {
            if (this._fireRequired == false) return;

            this._fireRequired = false;
            this.OnFlyBullet();
        }

        private void OnFlyBullet()
        {
            _bulletSystem.Value.FlyBulletByArgs(new BulletData
            {
                isPlayer = true,
                physicsLayer = this._bulletConfig.physicsLayer,
                color = this._bulletConfig.color,
                damage = this._bulletConfig.damage,
                position = _weaponComponent.Value.GetShootingPosition(),
                velocity = _weaponComponent.Value.GetShootingVelocity(Vector3.up, this._bulletConfig.speed)
            });
        }

        public void FireRequest() => _fireRequired = true;
    }
}
