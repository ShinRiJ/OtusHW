using System.Collections;
using System.Collections.Generic;
using ShootEmUp;
using TNRD;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour, IIEnemyInitializer
    {
        [Header("Spawn")]
        [SerializeField]
        private SerializableInterface<IEnemyPositionGetter> _enemyPositionsGetter;

        [SerializeField]
        private GameObject _target;

        [SerializeField]
        private Transform _worldTransform;

        public GameObject TrySpawnEnemy(EnemyComponentProvider enemyComponentProvider)
        {
            if (enemyComponentProvider == null)
            {
                Debug.LogError("!!! SpawnerError !!!");
                return null;
            }

            enemyComponentProvider.transform.SetParent(_worldTransform);

            var spawnPosition = _enemyPositionsGetter.Value.RandomSpawnPosition();
            enemyComponentProvider.transform.position = spawnPosition.position;

            var attackPosition = _enemyPositionsGetter.Value.RandomAttackPosition();

            enemyComponentProvider.EnemyMoveAgentInstance.SetDestination(attackPosition.position);
            enemyComponentProvider.EnemyAttackAgentInstance.SetTarget(_target);

            return enemyComponentProvider.gameObject;
        }
    }
}
