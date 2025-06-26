using System;
using TNRD;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace ShootEmUp
{
    public interface IEnemyAttackConfigure
    {
        public event Action<GameObject, Vector2, Vector2, IWeaponComponent> OnFire;
        public void SetTarget(GameObject target);
        public void Reset();
    }

    public sealed class EnemyAttackAgent : MonoBehaviour, IEnemyAttackConfigure
    {
        public event Action<GameObject, Vector2, Vector2, IWeaponComponent> OnFire;

        [SerializeField] private SerializableInterface<IWeaponComponent> weaponComponent;
        [SerializeField] private SerializableInterface<IEnemyMoveAgent> moveAgent;
        [SerializeField] private Single countdown;

        private GameObject target;
        private Single currentTime;

        public void SetTarget(GameObject target)
        {
            this.target = target;
        }

        public void Reset()
        {
            this.currentTime = this.countdown;
        }

        private void FixedUpdate()
        {
            if (!this.moveAgent.Value.IsReached())
                return;
            
            if (!this.target.GetComponent<HitPointsComponent>().IsHitPointsExists())
                return;

            this.currentTime -= Time.fixedDeltaTime;
            if (this.currentTime <= 0)
            {
                this.Fire();
                this.currentTime += this.countdown;
            }
        }

        private void Fire()
        {
            var startPosition = this.weaponComponent.Value.GetShootingPosition();
            var vector = (Vector2) this.target.transform.position - startPosition;
            var direction = vector.normalized;

            OnFire?.Invoke(this.gameObject, startPosition, direction, this.weaponComponent.Value);
        }
    }
}