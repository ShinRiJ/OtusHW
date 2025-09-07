using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class TeamComponent
    {
        private Boolean _isPlayer;

        public TeamComponent(Boolean status)
        {
            _isPlayer = status;
        }

        public bool IsPlayer => _isPlayer;
    }
}