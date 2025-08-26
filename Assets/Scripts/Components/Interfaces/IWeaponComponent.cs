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
}