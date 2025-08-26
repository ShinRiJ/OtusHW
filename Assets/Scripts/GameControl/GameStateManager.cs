using System.Collections.Generic;
using System.Linq;
using TNRD;
using UnityEngine;
using UnityEngine.Events;

namespace ShootEmUp
{
    public class GameStateManager : MonoBehaviour
    {
        [SerializeField] private SerializableInterface<IEndGameEvent> _endGameEventSource;
        [SerializeField] private InGameUiController _inGameUiController;

        private List<IStartGameListener> _startGameListeners = new List<IStartGameListener>();
        private List<IFinishGameListener> _finishGameListeners = new List<IFinishGameListener>();
        private List<IPauseGameListener> _pauseGameListeners = new List<IPauseGameListener>();
        private List<IResumeGameListener> _resumeGameListeners = new List<IResumeGameListener>();

        private void Awake()
        {
            _inGameUiController.OnGameStarted += StartGame;
            _inGameUiController.OnGamePaused += PauseGame;
            _inGameUiController.OnGameResumed += ResumeGame;
        }

        private void StartGame()
        {
            _endGameEventSource.Value.OnEndGame += FinishGame;

            foreach (var listener in _startGameListeners)
            {
                listener.StartGame();
            }
        }

        public void FinishGame()
        {
            PauseGame();

            print("End");
            _endGameEventSource.Value.OnEndGame -= FinishGame;

            foreach (var listener in _finishGameListeners)
            {
                listener.FinishGame();
            }
        }

        public void PauseGame()
        {
            foreach (var listener in _pauseGameListeners)
            {
                listener.PauseGame();
            }
        }

        public void ResumeGame()
        {
            foreach (var listener in _resumeGameListeners)
            {
                listener.ResumeGame();
            }
        }

        public void DeleteListeners(IEnumerable<IGameStateListener> bufferGameListeners)
        {
            foreach (var listener in bufferGameListeners)
            {
                DeleteListener(listener);
            }
        }

        public void DeleteListener(IGameStateListener listener)
        {
            if (listener is IStartGameListener startListener && _startGameListeners.Contains(startListener))
            {
                _startGameListeners.Remove(startListener);
            }

            if (listener is IFinishGameListener finishListener && _finishGameListeners.Contains(finishListener))
            {
                _finishGameListeners.Remove(finishListener);
            }

            if (listener is IPauseGameListener pauseListener && _pauseGameListeners.Contains(pauseListener))
            {
                _pauseGameListeners.Remove(pauseListener);
            }

            if (listener is IResumeGameListener resumeListener && _resumeGameListeners.Contains(resumeListener))
            {
                _resumeGameListeners.Remove(resumeListener);
            }
        }

        public void RegisterListeners(IEnumerable<IGameStateListener> bufferGameListeners)
        {
            foreach (var listener in bufferGameListeners)
            {
                RegisterListener(listener);
            }
        }

        public void RegisterListener(IGameStateListener listener)
        {
            if (listener is IStartGameListener startListener)
            {
                _startGameListeners.Add(startListener);
            }

            if (listener is IFinishGameListener finishListener)
            {
                _finishGameListeners.Add(finishListener);
            }

            if (listener is IPauseGameListener pauseListener)
            {
                _pauseGameListeners.Add(pauseListener);
            }

            if (listener is IResumeGameListener resumeListener)
            {
                _resumeGameListeners.Add(resumeListener);
            }
        }
    }
}
