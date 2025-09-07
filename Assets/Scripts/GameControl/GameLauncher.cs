using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

public class GameLauncher: IInitializable
{
    public event Action OnGameLaunched;

    TMP_Text _countdownTextUIElement;
    Int32 _initCounterValue = 3;
    
    private Int32 _counterValue;

    public GameLauncher(TMP_Text countdownTextUIElement,Int32 initCounterValue)
    {
        _countdownTextUIElement = countdownTextUIElement;
        _initCounterValue = initCounterValue;
    }


    public void Initialize()
    {
        _countdownTextUIElement.gameObject.SetActive(false);
    }

    public async UniTaskVoid LaunchGame()
    {
        _countdownTextUIElement.gameObject.SetActive(true);

        _counterValue = _initCounterValue;
        _countdownTextUIElement.text = _counterValue.ToString();

        while(_counterValue > 0)
        {
            await UniTask.WaitForSeconds(1);
            _counterValue -= 1;
            _countdownTextUIElement.text = _counterValue.ToString();
        }

        _countdownTextUIElement.gameObject.SetActive(false);
        
        OnGameLaunched?.Invoke();
    }
}
