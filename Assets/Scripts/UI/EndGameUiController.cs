using System;
using ShootEmUp;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUiController : MonoBehaviour, IFinishGameListener
{
    public event Action OnStartGameAgain;

    [SerializeField] private Button _startAgainButton;

    [SerializeField] private GameObject _backGround;

    private void Awake()
    {
        _backGround.SetActive(false);
        _startAgainButton.gameObject.SetActive(false);

        _startAgainButton.onClick.AddListener(StartGameAgain);
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

    public void FinishGame()
    {
        InitEndGameUI();
    }
}
