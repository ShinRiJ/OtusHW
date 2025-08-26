using System;
using System.Collections.Generic;
using System.Linq;
using ShootEmUp;
using UnityEngine;

public class GameTickableController : MonoBehaviour, ITickerRegister, IPauseGameListener, IResumeGameListener, IStartGameListener
{
    private List<ICommonTickable> _commonTickers = new List<ICommonTickable>();
    private List<IFixedTickable> _fixedTickers = new List<IFixedTickable>();

    private Boolean _isPaused = false;

    void Update()
    {
        if (_isPaused) return;

        for (Int32 i = 0; i < _commonTickers.Count; i++)
        {
            _commonTickers[i].Tick();
        }
    }

    void FixedUpdate()
    {
        if (_isPaused) return;

        for (Int32 i = 0; i < _fixedTickers.Count; i++)
        {
            _fixedTickers[i].FixedTick();
        }
    }

    public void RegisterTickers(IEnumerable<ITickable> bufferTickers)
    {
        foreach (var ticker in bufferTickers)
        {
            RegisterTicker(ticker);
        }
    }

    public void RegisterTicker(ITickable ticker)
    {
        if (ticker is ICommonTickable commonTickable)
            _commonTickers.Add(commonTickable);

        if (ticker is IFixedTickable fixedTickable)
            _fixedTickers.Add(fixedTickable);
    }

    public void DeleteTickers(IEnumerable<ITickable> bufferTickers)
    {
        foreach (var ticker in bufferTickers)
        {
            DeleteTicker(ticker);
        }
    }

    public void DeleteTicker(ITickable ticker)
    {
        if (ticker is ICommonTickable commonTickable && _commonTickers.Contains(commonTickable))
            _commonTickers.Remove(commonTickable);

        if (ticker is IFixedTickable fixedTickable && _fixedTickers.Contains(fixedTickable))
            _fixedTickers.Remove(fixedTickable);
    }

    public void PauseGame()
    {
        _isPaused = true;
    }

    public void ResumeGame()
    {
        _isPaused = false;
    }

    public void StartGame()
    {
        if(_isPaused)
        {
            _isPaused = false;
        }
    }
}
