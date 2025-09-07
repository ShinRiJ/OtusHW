using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class WeaponComponent : IWeaponComponent
    {
        private Transform _firePoint;

        public WeaponComponent(Transform firePoint)
        {
            _firePoint = firePoint;
        }

        public Vector2 GetShootingPosition()
        {
            return _firePoint.position;
        }

        public Quaternion GetShootingRotation()
        { 
            return _firePoint.rotation;
        }
        
        public Vector2 GetShootingVelocity(Vector3 vector, Single speed)
        {
            return _firePoint.rotation * vector * speed;
        }

    }
}