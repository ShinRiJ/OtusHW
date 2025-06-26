using System.Collections;
using System.Collections.Generic;
using ShootEmUp;
using TNRD;
using UnityEngine;

namespace ShootEmUp
{
    public interface IIEnemyInitializer
    {
        public GameObject TrySpawnEnemy(GameObject enemy);
    }

    public class EnemySpawner : MonoBehaviour, IIEnemyInitializer
    {
        [Header("Spawn")]
        [SerializeField]
        private SerializableInterface<IEnemyPositionGetter> _enemyPositionsGetter;

        [SerializeField]
        private GameObject _target;

        [SerializeField]
        private Transform _worldTransform;

        public GameObject TrySpawnEnemy(GameObject enemy)
        {
            EnemyComponentProvider enemyComponentProvider = enemy.GetComponent<EnemyComponentProvider>();

            if (enemyComponentProvider == null)
            {
                Debug.LogError("!!! SpawnerError !!!");
                return null;
            }

            enemy.transform.SetParent(this._worldTransform);

            var spawnPosition = this._enemyPositionsGetter.Value.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            var attackPosition = this._enemyPositionsGetter.Value.RandomAttackPosition();

            enemyComponentProvider.EnemyMoveAgentInstance.SetDestination(attackPosition.position);
            enemyComponentProvider.EnemyAttackAgentInstance.SetTarget(this._target);

            return enemy;
        }
    }
}
