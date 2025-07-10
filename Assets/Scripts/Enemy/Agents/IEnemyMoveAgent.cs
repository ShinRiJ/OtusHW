using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IEnemyMoveAgent
    {
        public Boolean IsReached();
        public void SetDestination(Vector2 endPoint);
    }
}