using System;
using TNRD;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterStateController : MonoBehaviour, ICharacterDeathNotifier, IStartGameListener, IFinishGameListener
    {
        public event Action<CharacterStateController> OnCharacterDeath;

        [SerializeField] Int32 _initCharacterHP = 5;
        [SerializeField] private SerializableInterface<IHitPointEndNotifier> _hitPointEndInterface;
        [SerializeField] private SerializableInterface<IHitPointInitRestore> _hitPointInitRestore;

        private void OnCharacterDeathHandler(GameObject _)
        {
            OnCharacterDeath?.Invoke(this);
        }

        public void StartGame()
        {
            _hitPointInitRestore.Value.InitRestoreHealth(_initCharacterHP);
            _hitPointEndInterface.Value.OnHPEmpty += OnCharacterDeathHandler;
        }

        public void FinishGame()
        {
            _hitPointEndInterface.Value.OnHPEmpty -= OnCharacterDeathHandler;
        }
    }
}