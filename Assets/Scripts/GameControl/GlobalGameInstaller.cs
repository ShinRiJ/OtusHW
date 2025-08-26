using UnityEngine;

namespace ShootEmUp
{
    public class GlobalGameInstaller : MonoBehaviour
    {
        [SerializeField] GameTickableController _gameTickableController;
        [SerializeField] GameStateManager _gameStateManager;

        private void Start()
        {
            var tickersBuffer = GetComponentsInChildren<ITickable>();
            var listenersBuffer = GetComponentsInChildren<IGameStateListener>();

            if (tickersBuffer != null)
                _gameTickableController.RegisterTickers(tickersBuffer);

            if (_gameStateManager != null)
                _gameStateManager.RegisterListeners(listenersBuffer);
        }
    }
}