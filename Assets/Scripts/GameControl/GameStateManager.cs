using System;
using System.Collections.Generic;
using System.Linq;
using TNRD;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace ShootEmUp
{
    public class GameStateManager : IDisposable, IInitializable
    {

        [Inject] private SignalBus _signalBus;

        private IEndGameEvent _endGameEventSource;

        public void Initialize()
        {
            _signalBus.Subscribe<StartGameSignal>(OnStartGame);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<StartGameSignal>(OnStartGame);
        }

        public GameStateManager(IEndGameEvent endGameEvent)
        {
            _endGameEventSource = endGameEvent;
        }

        private void OnStartGame()
        {
            _endGameEventSource.OnEndGame += FinishGame;
        }

        public void FinishGame()
        {
            PauseGame();

            _endGameEventSource.OnEndGame -= FinishGame;

            _signalBus.Fire<FinishGameSignal>();
        }

        public void PauseGame()
        {
            _signalBus.Fire<PauseGameSignal>();
        }
    }
}
