using TNRD;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
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
            FinishGame();
        }

        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}