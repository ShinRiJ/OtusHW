using System;
using System.Collections.Generic;
using ShootEmUp;
using Zenject;

public class GameTickableController : ITickable, IFixedTickable, IInitializable, IDisposable
{
    [Inject] private SignalBus _signalBus;

    private List<ICommonTickableCustom> _commonTickers = new List<ICommonTickableCustom>();
    private List<IFixedTickableCustom> _fixedTickers = new List<IFixedTickableCustom>();

    private Boolean _isPaused = false;

    public void Initialize()
    {
        _signalBus.Subscribe<StartGameSignal>(OnStartGame);
        _signalBus.Subscribe<ResumeGameSignal>(OnResumeGame);
        _signalBus.Subscribe<PauseGameSignal>(OnPauseGame);
    }

    public void Dispose()
    {
        _signalBus.TryUnsubscribe<StartGameSignal>(OnStartGame);
        _signalBus.TryUnsubscribe<ResumeGameSignal>(OnResumeGame);
        _signalBus.TryUnsubscribe<PauseGameSignal>(OnPauseGame);
    }

    public GameTickableController(List<ICommonTickableCustom> commonTickableCustom, List<IFixedTickableCustom> fixedTickableCustoms)
    {
        _commonTickers = commonTickableCustom;
        _fixedTickers = fixedTickableCustoms;
    }

    public void Tick()
    {
        if (_isPaused) return;

        for (Int32 i = 0; i < _commonTickers.Count; i++)
        {
            _commonTickers[i].Tick();
        }
    }

    public void FixedTick()
    {
        if (_isPaused) return;

        for (Int32 i = 0; i < _fixedTickers.Count; i++)
        {
            _fixedTickers[i].FixedTick();
        }
    }

    //public void RegisterTickers(IEnumerable<ITickableCustom> bufferTickers)
    //{
    //    foreach (var ticker in bufferTickers)
    //    {
    //        RegisterTicker(ticker);
    //    }
    //}

    //public void RegisterTicker(ITickableCustom ticker)
    //{
    //    if (ticker is ICommonTickableCustom commonTickable)
    //        _commonTickers.Add(commonTickable);

    //    if (ticker is IFixedTickable fixedTickable)
    //        _fixedTickers.Add(fixedTickable);
    //}

    //public void DeleteTickers(IEnumerable<ITickableCustom> bufferTickers)
    //{
    //    foreach (var ticker in bufferTickers)
    //    {
    //        DeleteTicker(ticker);
    //    }
    //}

    //public void DeleteTicker(ITickableCustom ticker)
    //{
    //    if (ticker is ICommonTickableCustom commonTickable && _commonTickers.Contains(commonTickable))
    //        _commonTickers.Remove(commonTickable);

    //    if (ticker is IFixedTickable fixedTickable && _fixedTickers.Contains(fixedTickable))
    //        _fixedTickers.Remove(fixedTickable);
    //}

    public void OnPauseGame()
    {
        _isPaused = true;
    }

    public void OnResumeGame()
    {
        _isPaused = false;
    }

    public void OnStartGame()
    {
        if(_isPaused)
        {
            _isPaused = false;
        }
    }
}
