using System;

namespace ShootEmUp
{
    public interface ICharacterDeathNotifier
    {
        public event Action<CharacterStateController> OnCharacterDeath;
    }
}