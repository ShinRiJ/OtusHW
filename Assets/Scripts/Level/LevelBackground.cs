using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : IFixedTickableCustom
    {
        private float _startPositionY;

        private float _endPositionY;

        private float _movingSpeedY;

        private float _positionX;

        private float _positionZ;

        private Transform _myTransform;

        public LevelBackground(BackGroundParams backGroundParams, Transform transform)
        {
            _startPositionY = backGroundParams._startPositionY;
            _endPositionY = backGroundParams._endPositionY;
            _movingSpeedY = backGroundParams._movingSpeedY;
            _myTransform = transform;
            var position = _myTransform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }

        public void FixedTick()
        {
            if (_myTransform.position.y <= _endPositionY)
            {
                _myTransform.position = new Vector3(
                    _positionX,
                    _startPositionY,
                    _positionZ
                );
            }

            _myTransform.position -= new Vector3(
                _positionX,
                _movingSpeedY * Time.fixedDeltaTime,
                _positionZ
            );
        }
    }
}