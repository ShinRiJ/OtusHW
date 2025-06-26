using System;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace ShootEmUp
{
    public interface IEnemyPool
    {
        public void RemoveEnemy(GameObject enemy);
        public GameObject TryGetNewEnemy();
    }
    public sealed class EnemyPool : MonoBehaviour, IEnemyPool
    {
        [Header("Spawn")]
        [SerializeField]
        private SerializableInterface<IIEnemyInitializer> _enemySpawner;

        [Header("Pool")]
        [SerializeField]
        private Transform _poolContainer;

        [SerializeField]
        private GameObject _enemyPrefab;

        [SerializeField]
        private Int32 _poolSize = 7;

        private readonly Queue<GameObject> enemyPool = new();
        
        private void Awake()
        {
            for (var i = 0; i < _poolSize; i++)
            {
                var enemy = Instantiate(this._enemyPrefab, this._poolContainer);
                this.enemyPool.Enqueue(enemy);
            }
        }

        public void RemoveEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(this._poolContainer);
            this.enemyPool.Enqueue(enemy);
        }

        public GameObject TryGetNewEnemy()
        {
            if (!this.enemyPool.TryDequeue(out var enemy))
                return null;
            else
                return _enemySpawner.Value.TrySpawnEnemy(enemy);
        }
    }
}