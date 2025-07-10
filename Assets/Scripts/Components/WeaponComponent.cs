using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour, IWeaponComponent
    {
        [SerializeField] private Transform _firePoint;
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