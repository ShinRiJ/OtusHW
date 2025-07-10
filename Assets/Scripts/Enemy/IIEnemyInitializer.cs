using UnityEngine;

namespace ShootEmUp
{
    public interface IIEnemyInitializer
    {
        public GameObject TrySpawnEnemy(EnemyComponentProvider enemyComponentProvider);
    }
}