using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class BackGroundParams
    {
        [SerializeField]
        public float _startPositionY;

        [SerializeField]
        public float _endPositionY;

        [SerializeField]
        public float _movingSpeedY;
    }
}