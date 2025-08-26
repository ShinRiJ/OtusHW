using System;

namespace ShootEmUp
{
    internal interface IEndGameEvent
    {
        public event Action OnEndGame;
    }
}
