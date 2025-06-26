using System;
using TNRD;
using UnityEngine;

namespace ShootEmUp
{
    public interface ICharacterDeathNotifier
    {
        public event Action<CharacterStateController> OnCharacterDeath;
    }
    public sealed class CharacterStateController : MonoBehaviour, ICharacterDeathNotifier
    {
        public event Action<CharacterStateController> OnCharacterDeath;

        [SerializeField] private SerializableInterface<IHitPointEndNotifier> _hitPointEndInterface;

        private void OnEnable() => _hitPointEndInterface.Value.HpEmpty += this.OnCharacterDeathHandler;
        private void OnDisable() => _hitPointEndInterface.Value.HpEmpty -= this.OnCharacterDeathHandler;
        private void OnCharacterDeathHandler(GameObject _) => OnCharacterDeath?.Invoke(this);
    }
}