using UnityEngine;

namespace ShootEmUp
{
    public interface IEnemyPositionGetter
    {
        public Transform RandomSpawnPosition();
        public Transform RandomAttackPosition();
    }

    public sealed class EnemyPositions : MonoBehaviour, IEnemyPositionGetter
    {
        [SerializeField]
        private Transform[] spawnPositions;

        [SerializeField]
        private Transform[] attackPositions;

        public Transform RandomSpawnPosition()
        {
            return this.RandomTransform(this.spawnPositions);
        }

        public Transform RandomAttackPosition()
        {
            return this.RandomTransform(this.attackPositions);
        }

        private Transform RandomTransform(Transform[] transforms)
        {
            var index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}