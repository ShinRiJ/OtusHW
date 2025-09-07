using System;

namespace ShootEmUp
{
    public interface IInputManager
    {
        public float MoveDirection { get; }
        public event Action OnFireAction;
    }
}
