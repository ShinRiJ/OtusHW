using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IWeaponComponent
    {
        public Vector2 GetShootingPosition();
        public Quaternion GetShootingRotation();
        public Vector2 GetShootingVelocity(Vector3 vector, Single speed);
    }
    public sealed class WeaponComponent : MonoBehaviour, IWeaponComponent
    {
        [SerializeField]
        private Transform firePoint;
        public Vector2 GetShootingPosition() => this.firePoint.position;
        public Quaternion GetShootingRotation() => this.firePoint.rotation;
        public Vector2 GetShootingVelocity(Vector3 vector, Single speed) => this.firePoint.rotation * vector * speed;

    }
}