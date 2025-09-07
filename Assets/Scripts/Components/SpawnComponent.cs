using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class SpawnComponent: IInitializable, IDisposable
    {
        [Inject] private SignalBus _signalBus;

        [Inject(Id = "PlayerSpawnPoint")] private Transform _spawnPoint;
        private Transform _controlledTransform;

        public SpawnComponent(Transform controlledTransform)
        {
            _controlledTransform = controlledTransform;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<StartGameSignal>(OnStartGame);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<StartGameSignal>(OnStartGame);
        }

        public void OnStartGame()
        {
            _controlledTransform.position = _spawnPoint.position;
        }
    }
}
