using System;
using TNRD;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class CharacterStateController : ICharacterDeathNotifier, IDisposable, IInitializable
    {
        [Inject] private SignalBus _signalBus;

        public event Action<CharacterStateController> OnCharacterDeath;

        private IHitPointEndNotifier _hitPointEndInterface;
        private IHitPointInitRestore _hitPointInitRestore;

        public void Initialize()
        {
            _signalBus.Subscribe<StartGameSignal>(OnStartGame);
            _signalBus.Subscribe<FinishGameSignal>(OnFinishGame);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<StartGameSignal>(OnStartGame);
            _signalBus.Unsubscribe<FinishGameSignal>(OnFinishGame);
        }

        public CharacterStateController(IHitPointEndNotifier hitPointEndNotifier, IHitPointInitRestore hitPointInitRestore)
        {
            _hitPointEndInterface = hitPointEndNotifier;
            _hitPointInitRestore = hitPointInitRestore;
        }

        private void OnCharacterDeathHandler(GameObject _)
        {
            OnCharacterDeath?.Invoke(this);
        }

        public void OnStartGame()
        {
            _hitPointInitRestore.InitRestoreHealth();
            _hitPointEndInterface.OnHPEmpty += OnCharacterDeathHandler;
        }

        public void OnFinishGame()
        {
            _hitPointEndInterface.OnHPEmpty -= OnCharacterDeathHandler;
        }
    }
}