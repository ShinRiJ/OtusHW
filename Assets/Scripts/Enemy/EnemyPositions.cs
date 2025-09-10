using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPositions : IEnemyPositionGetter
    {
        private Transform[] _spawnPositions;
        private Transform[] _attackPositions;

        public EnemyPositions(Transform[] spawnPositions, Transform[] attackPositions)
        {
            _spawnPositions = spawnPositions;
            _attackPositions = attackPositions;
        }

        public Transform RandomSpawnPosition()
        {
            return RandomTransform(_spawnPositions);
        }

        public Transform RandomAttackPosition()
        {
            return RandomTransform(_attackPositions);
        }

        private Transform RandomTransform(Transform[] transforms)
        {
            var index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}