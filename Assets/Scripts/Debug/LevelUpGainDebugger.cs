using PopUp;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpGainDebugger : MonoBehaviour
{
    [SerializeField] private PopUpManager _popUpManager;

    [SerializeField] private Int32 _xpGainInOneTick = 100;
    [SerializeField] private Single _tickPeriodInSec = 1;

    private Single _currentTime = 0;

    private void FixedUpdate()
    {
        _currentTime += Time.fixedDeltaTime;
        
        if(_currentTime >= _tickPeriodInSec)
        {
            _popUpManager.AddXPToCurrentCharacter(_xpGainInOneTick);
            _currentTime = 0;
        }
    }
}
