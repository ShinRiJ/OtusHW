using UnityEngine;

namespace ShootEmUp
{
    public interface IEnemyPool
    {
        public void RemoveEnemy(GameObject enemy);
        public GameObject TryGetNewEnemy();
    }
}