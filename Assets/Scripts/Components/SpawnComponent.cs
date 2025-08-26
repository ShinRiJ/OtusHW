using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComponent : MonoBehaviour, IStartGameListener
{
    [SerializeField] private Transform _spawnPoint;

    public void StartGame()
    {
        transform.position = _spawnPoint.position;
    }
}
