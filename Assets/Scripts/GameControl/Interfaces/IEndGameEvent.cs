using System;

namespace ShootEmUp
{
    public interface IEndGameEvent
    {
        public event Action OnEndGame;
    }
}
