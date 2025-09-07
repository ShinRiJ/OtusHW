using ShootEmUp;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EndGameUiController : IInitializable, IDisposable
{
    [Inject] private SignalBus _signalBus;

    public event Action OnStartGameAgain;

    private Button _startAgainButton;
    private GameObject _backGround;

    public EndGameUiController(Button startAgainButton, GameObject backGround)
    {
        _startAgainButton = startAgainButton;
        _backGround = backGround;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<FinishGameSignal>(OnFinishGame);

        _backGround.SetActive(false);
        _startAgainButton.gameObject.SetActive(false);
        _startAgainButton.onClick.AddListener(StartGameAgain);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<FinishGameSignal>(OnFinishGame);
        _startAgainButton.onClick.RemoveListener(StartGameAgain);
    }

    public void InitEndGameUI()
    {
        _backGround.SetActive(true);
        _startAgainButton.gameObject.SetActive(true);
    }

    private void StartGameAgain()
    {
        _backGround.SetActive(false);
        _startAgainButton.gameObject.SetActive(false);

        OnStartGameAgain?.Invoke();
    }

    public void OnFinishGame()
    {
        InitEndGameUI();
    }
}
