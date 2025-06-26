using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IHitPointEndNotifier
    {
        public event Action<GameObject> HpEmpty;
    }

    public interface IHitPointDamageRecieve
    {
        public void TakeDamage(Int32 damage);
    }
    public sealed class HitPointsComponent : MonoBehaviour, IHitPointEndNotifier, IHitPointDamageRecieve
    {
        public event Action<GameObject> HpEmpty;
        
        [SerializeField] private Int32 _hitPoints;
        
        public bool IsHitPointsExists() {
            return this._hitPoints > 0;
        }

        public void TakeDamage(Int32 damage)
        {
            this._hitPoints -= damage;
            if (this._hitPoints <= 0)
            {
                this.HpEmpty?.Invoke(this.gameObject);
            }
        }
    }
}