using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IHitPointEndNotifier
    {
        public event Action<GameObject> OnHPEmpty;
    }
}