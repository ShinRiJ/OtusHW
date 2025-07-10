using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShootEmUp
{
    internal interface IInputManager
    {
        public float MoveDirection { get; }
        public event Action OnFireAction;
    }
}
