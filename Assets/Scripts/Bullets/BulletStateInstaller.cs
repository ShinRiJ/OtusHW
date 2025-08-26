using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace ShootEmUp
{
    internal class BulletStateInstaller : MonoBehaviour
    {
        [SerializeField] GameStateManager _gameStateManager;

        public void RegisterBuletStateHandles(IEnumerable<IGameStateListener> bufferGameListeners)
        {
            _gameStateManager.RegisterListeners(bufferGameListeners);
        }

        public void DeleteBuletStateHandles(IEnumerable<IGameStateListener> bufferGameListeners)
        {
            _gameStateManager.DeleteListeners(bufferGameListeners);
        }
    }
}
