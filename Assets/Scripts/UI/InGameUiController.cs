using ShootEmUp;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InGameUiController : IInitializable, IDisposable
{
    [Inject] private SignalBus _signalBus;
    
    private Button _startButton;
    private Button _pauseButton;
    private Button _resumeButton;

    private GameObject _backGround;

    [InjectLocal] GameLauncher _launcher;
    [InjectLocal] EndGameUiController _endGameUiController;

    public InGameUiController(Button startButton, Button pauseButton, Button resumeButton, GameObject background)
    {
        _startButton = startButton;
        _pauseButton = pauseButton;
        _resumeButton = resumeButton;
        _backGround = background;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<FinishGameSignal>(OnFinishGame);

        _startButton.onClick.AddListener(StartGame);
        _pauseButton.onClick.AddListener(PauseGame);
        _resumeButton.onClick.AddListener(ResumeGame);

        _backGround.SetActive(true);

        _startButton.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
        _resumeButton.gameObject.SetActive(false);

        _launcher.OnGameLaunched += PostStartGame;
        _endGameUiController.OnStartGameAgain += StartGame;
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<FinishGameSignal>(OnFinishGame);

        _launcher.OnGameLaunched -= PostStartGame;
        _endGameUiController.OnStartGameAgain -= StartGame;

        _startButton.onClick.RemoveListener(StartGame);
        _pauseButton.onClick.RemoveListener(PauseGame);
        _resumeButton.onClick.RemoveListener(ResumeGame);
    }

    public void StartGame()
    {
        _backGround.SetActive(true);
        _startButton.gameObject.SetActive(false);

        _launcher.LaunchGame().Forget();
    }

    public void InitEndGameUI()
    {
        _pauseButton.gameObject.SetActive(false);
        _resumeButton.gameObject.SetActive(false);
    }

    private void PostStartGame()
    {
        _backGround.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
        _resumeButton.gameObject.SetActive(false);

        _signalBus.Fire<StartGameSignal>();
    }

    private void PauseGame()
    {
        _pauseButton.gameObject.SetActive(false);
        _resumeButton.gameObject.SetActive(true);

        _signalBus.Fire<PauseGameSignal>();
    }

    private void ResumeGame()
    {
        _pauseButton.gameObject.SetActive(true);
        _resumeButton.gameObject.SetActive(false);

        _signalBus.Fire<ResumeGameSignal>();
    }

    public void OnFinishGame()
    {
        InitEndGameUI();
    }
}
