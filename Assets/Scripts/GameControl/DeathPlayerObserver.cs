using System;
using TNRD;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class DeathPlayerObserver : IInitializable, IDisposable, IEndGameEvent
    {
        public event Action OnEndGame;
        private ICharacterDeathNotifier _characterControllerNotifier;

        public DeathPlayerObserver(ICharacterDeathNotifier characterDeathNotifier)
        {
            _characterControllerNotifier = characterDeathNotifier;
        }

        private void HandleCharacterDeath(CharacterStateController controller)
        {
            OnEndGame?.Invoke();
        }

        public void Initialize()
        {
            _characterControllerNotifier.OnCharacterDeath += HandleCharacterDeath;
        }

        public void Dispose()
        {
            _characterControllerNotifier.OnCharacterDeath -= HandleCharacterDeath;
        }
    }
}