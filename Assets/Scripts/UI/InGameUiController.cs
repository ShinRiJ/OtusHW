using ShootEmUp;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InGameUiController : MonoBehaviour, IFinishGameListener
{
    public event Action OnGameStarted;
    public event Action OnGamePaused;
    public event Action OnGameResumed;

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;

    [SerializeField] private GameObject _backGround;

    [SerializeField] GameLauncher _launcher;
    [SerializeField] EndGameUiController _endGameUiController;

    private void Awake()
    {
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

        OnGameStarted?.Invoke();
    }

    private void PauseGame()
    {
        _pauseButton.gameObject.SetActive(false);
        _resumeButton.gameObject.SetActive(true);

        OnGamePaused?.Invoke();
    }

    private void ResumeGame()
    {
        _pauseButton.gameObject.SetActive(true);
        _resumeButton.gameObject.SetActive(false);

        OnGameResumed?.Invoke();
    }

    public void FinishGame()
    {
        InitEndGameUI();
    }
}
