using PopUp;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsView : MonoBehaviour
{
    [Header("XP бар")]
    [SerializeField, ShowInInspector]
    private List<StatView> _statsVievList;

    public void SetStatValue(StatsType statsType, Int32 value)
    {
        for (Int32 i = 0; i < _statsVievList.Count; i++)
        {
            if (_statsVievList[i].Type == statsType)
            {
                _statsVievList[i].SetStatValue(value);
                return;
            }
        }

        throw new Exception("Ошибка при обновлении стата");
    }
}
