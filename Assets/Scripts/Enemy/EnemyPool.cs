using System;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour, IEnemyPool
    {
        [Header("Spawn")]
        [SerializeField] private SerializableInterface<IIEnemyInitializer> _enemySpawner;

        [Header("Pool")]
        [SerializeField] private Transform _poolContainer;

        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Int32 _poolSize = 7;

        [SerializeField]
        private SerializableInterface<IEnemyUpdateInstaller> _enemyUpdateInstaller;

        private readonly Queue<GameObject> _enemyPool = new();
        
        private void Awake()
        {
            for (var i = 0; i < _poolSize; i++)
            {
                var enemy = Instantiate(_enemyPrefab, _poolContainer);
                _enemyPool.Enqueue(enemy);
            }
        }

        public void RemoveEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(_poolContainer);
            _enemyPool.Enqueue(enemy);

            _enemyUpdateInstaller.Value.DeleteEnemyTicker(enemy.GetComponent<EnemyComponentProvider>());
        }

        public GameObject TryGetNewEnemy()
        {
            if (!_enemyPool.TryDequeue(out var enemy))
            {
                return null;
            }
            else
            {
                EnemyComponentProvider newEnemy = enemy.GetComponent<EnemyComponentProvider>();
                GameObject newEnemyObj = _enemySpawner.Value.TrySpawnEnemy(newEnemy);
                
                if(newEnemyObj != null)
                    _enemyUpdateInstaller.Value.RegisterEnemyTicker(newEnemy);
                return _enemySpawner.Value.TrySpawnEnemy(newEnemy);
            }
        }
    }
}