using System;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class EnemyFacade: UnitFacade
    {
        public event Action<EnemyFacade> OnDeath;
        public WeaponComponent WeaponComponent { get; private set; }
        public EnemyMoveAgent EnemyMoveAgent { get; private set; }
        public EnemyAttackAgent EnemyAttackAgent { get; private set; }

        [Inject]
        public void Construct(HitPointsComponent hitPointsComponent,WeaponComponent weaponComponent,
                              EnemyMoveAgent enemyMoveAgent, EnemyAttackAgent enemyAttackAgent,
                              TeamComponent teamComponent)
        {
            TeamComponent = teamComponent;
            HitPointsComponent = hitPointsComponent;
            WeaponComponent = weaponComponent;
            EnemyMoveAgent = enemyMoveAgent;
            EnemyAttackAgent = enemyAttackAgent;

            HitPointsComponent.OnHPEmpty += HandleDeath;
        }

        private void HandleDeath(GameObject gameObject)
        {
            OnDeath?.Invoke(this);
        }
    }

    public class EnemyPool : MonoMemoryPool<GameObject, EnemyFacade>
    {
        [Inject] IEnemyPositionGetter enemyPositionGetter;
        protected override void Reinitialize(GameObject target, EnemyFacade item)
        {
            item.HitPointsComponent.InitRestoreHealth();
            item.transform.position = enemyPositionGetter.RandomSpawnPosition().position;
            item.EnemyMoveAgent.SetDestination(enemyPositionGetter.RandomAttackPosition().position);
            item.EnemyAttackAgent.SetTarget(target);
            item.gameObject.SetActive(true);
        }
        protected override void OnDespawned(EnemyFacade item)
        {
            item.gameObject.SetActive(false);
        }
    }

    public class UnitFacade : MonoBehaviour
    {
        public HitPointsComponent HitPointsComponent { get; protected set; }
        public TeamComponent TeamComponent { get; protected set; }
    }
}
