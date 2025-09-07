using System;
using TNRD;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour, IEnemyAttackConfigure, IFixedTickableCustom
    {
        public event Action<GameObject, Vector2, Vector2, IWeaponComponent> OnFire;

        [SerializeField] private SerializableInterface<IWeaponComponent> _weaponComponent;
        [SerializeField] private SerializableInterface<IEnemyMoveAgent> _moveAgent;
        [SerializeField] private Single _countdown = 1;

        private GameObject _target;
        private Single _currentTime;

        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        public void Reset()
        {
            _currentTime = _countdown;
        }

        public void FixedTick()
        {
            if (!_moveAgent.Value.IsReached())
            {
                return;
            }
            
            if (!_target.GetComponent<HitPointsComponent>().IsHitPointsExists())
            {
                return;
            }

            _currentTime -= Time.fixedDeltaTime;

            if (_currentTime <= 0)
            {
                Fire();
                _currentTime += _countdown;
            }
        }

        private void Fire()
        {
            var startPosition = _weaponComponent.Value.GetShootingPosition();
            var vector = (Vector2) _target.transform.position - startPosition;
            var direction = vector.normalized;

            OnFire?.Invoke(gameObject, startPosition, direction, _weaponComponent.Value);
        }
    }
}