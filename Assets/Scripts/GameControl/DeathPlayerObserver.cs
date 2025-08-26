using System;
using TNRD;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class DeathPlayerObserver : MonoBehaviour, IEndGameEvent
    {
        public event Action OnEndGame;
        [SerializeField] private SerializableInterface<ICharacterDeathNotifier> _characterControllerNotifier;

        private void OnEnable()
        {
            _characterControllerNotifier.Value.OnCharacterDeath += HandleCharacterDeath;
        }

        private void OnDisable()
        {
            _characterControllerNotifier.Value.OnCharacterDeath -= HandleCharacterDeath;
        }

        private void HandleCharacterDeath(CharacterStateController controller)
        {
            OnEndGame?.Invoke();
        }
    }
}