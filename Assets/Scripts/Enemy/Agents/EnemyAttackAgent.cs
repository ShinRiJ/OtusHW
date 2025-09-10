using System;
using TNRD;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : IEnemyAttackConfigure, IFixedTickableCustom
    {
        public event Action<GameObject, Vector2, Vector2, IWeaponComponent> OnFire;

        [Inject] private IWeaponComponent _weaponComponent;
        [Inject] private IEnemyMoveAgent _moveAgent;

        private Single _countdown = 1;

        private GameObject _target;
        private GameObject _myGameObject;
        private Single _currentTime;

        public EnemyAttackAgent(GameObject myGameObject)
        {
            _myGameObject = myGameObject;
        }

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
            if (!_moveAgent.IsReached())
            {
                return;
            }
            
            if (!_target.GetComponent<UnitFacade>().HitPointsComponent.IsHitPointsExists())
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
            var startPosition = _weaponComponent.GetShootingPosition();
            var vector = (Vector2) _target.transform.position - startPosition;
            var direction = vector.normalized;

            OnFire?.Invoke(_myGameObject, startPosition, direction, _weaponComponent);
        }
    }
}