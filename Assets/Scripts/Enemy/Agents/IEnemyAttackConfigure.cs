using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IEnemyAttackConfigure
    {
        public event Action<GameObject, Vector2, Vector2, IWeaponComponent> OnFire;
        public void SetTarget(GameObject target);
        public void Reset();
    }
}