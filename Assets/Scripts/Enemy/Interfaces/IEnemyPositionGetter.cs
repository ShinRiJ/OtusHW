using UnityEngine;

namespace ShootEmUp
{
    public interface IEnemyPositionGetter
    {
        public Transform RandomSpawnPosition();
        public Transform RandomAttackPosition();
    }
}