using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface ILevelBoundCheck
    {
        public Boolean InBounds(Vector3 position);
    }
}