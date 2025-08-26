using System;

namespace ShootEmUp
{
    internal interface IInputManager
    {
        public float MoveDirection { get; }
        public event Action OnFireAction;
    }
}
