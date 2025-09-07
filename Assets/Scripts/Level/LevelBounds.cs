using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBounds : ILevelBoundCheck
    {
        private Transform _leftBorder;
        private Transform _rightBorder;
        private Transform _downBorder;
        private Transform _topBorder;

        public LevelBounds(BorderTransforms borderTransforms)
        {
            _leftBorder = borderTransforms.LeftBorder;
            _rightBorder = borderTransforms.RightBorder;
            _downBorder = borderTransforms.DownBorder;
            _topBorder = borderTransforms.TopBorder;
        }

        public Boolean InBounds(Vector3 position)
        {
            var positionX = position.x;
            var positionY = position.y;
            return positionX > _leftBorder.position.x
                   && positionX < _rightBorder.position.x
                   && positionY > _downBorder.position.y
                   && positionY < _topBorder.position.y;
        }
    }
}