using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour, IHitPointEndNotifier, IHitPointDamageRecieve
    {
        public event Action<GameObject> OnHPEmpty;
        
        [SerializeField] private Int32 _hitPoints;
        
        public bool IsHitPointsExists()
        {
            return _hitPoints > 0;
        }

        public void TakeDamage(Int32 damage)
        {
            _hitPoints -= damage;

            if (_hitPoints <= 0)
            {
                OnHPEmpty?.Invoke(gameObject);
            }
        }
    }
}