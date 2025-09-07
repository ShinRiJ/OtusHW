using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyComponentProvider : MonoBehaviour
    {
        [SerializeReference] private HitPointsComponent _hitPointInstance;
        [SerializeReference] private EnemyMoveAgent _enemyMoveAgentInstance;
        [SerializeReference] private EnemyAttackAgent _enemyAttackAgentInstance;

        public IHitPointEndNotifier HitPointInstance => _hitPointInstance;
        public IEnemyMoveAgent EnemyMoveAgentInstance => _enemyMoveAgentInstance;
        public IEnemyAttackConfigure EnemyAttackAgentInstance => _enemyAttackAgentInstance;

        public ITickableCustom[] GetTickableComponets()
        {
            return GetComponents<ITickableCustom>();
        }
    }
}
