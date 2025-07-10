using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class TeamComponent : MonoBehaviour
    {
        [SerializeField] private Boolean _isPlayer;
        public bool IsPlayer => _isPlayer;
    }
}