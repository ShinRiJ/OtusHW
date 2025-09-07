using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : IHitPointDamageRecieve, IHitPointInitRestore, IHitPointEndNotifier
    {
        private Int32 _initialHitPoints;
        private Int32 _hitPoints;
        public event Action<GameObject> OnHPEmpty;
        private GameObject _gameObject;

        public HitPointsComponent(Int32 hitPoints, GameObject gameObject)
        {
            _initialHitPoints = _hitPoints = hitPoints;
        }

        public bool IsHitPointsExists()
        {
            return _hitPoints > 0;
        }

        public void TakeDamage(Int32 damage)
        {
            _hitPoints -= damage;

            if (_hitPoints <= 0)
            {
                OnHPEmpty?.Invoke(_gameObject);
            }
        }

        public void InitRestoreHealth()
        {
            _hitPoints = _initialHitPoints;
        }
    }
}