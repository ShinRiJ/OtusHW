using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;

    [SerializeField] private GameObject _backGroundInGame;

    [SerializeField] private Button _startAgainButton;
    [SerializeField] private GameObject _backGroundEndGame;

    [SerializeField] private TMP_Text _countdownTextUIElement;
    [SerializeField] private Int32 _initCounterValue = 3;

    override public void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameLauncher>().AsSingle().WithArguments(_countdownTextUIElement, _initCounterValue).NonLazy();
        Container.BindInterfacesAndSelfTo<EndGameUiController>().AsSingle().WithArguments(_startAgainButton, _backGroundEndGame).NonLazy();
        Container.BindInterfacesAndSelfTo<InGameUiController>().AsSingle().WithArguments(_startButton, _pauseButton, _resumeButton, _backGroundInGame).NonLazy();
    }
}
