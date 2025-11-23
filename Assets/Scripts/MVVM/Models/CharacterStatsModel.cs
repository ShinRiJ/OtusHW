using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PopUp
{
    [CreateAssetMenu(fileName = "NewCharacterStatsModel", menuName = "CharacterStatsModel")]
    public class CharacterStatsModel : BaseModel
    {
        [Header("Основной стат")]
        public List<StatsType> MainStats;

        [Header("Базовый прирост за уровень")]
        public Int32 BaseStatIncrease;

        [Header("Статы на 0 уровне")]
        [SerializeField, TableList]
        public List<StatEntry> CharacterStatsOnZeroLevel;

        public List<StatEntry> CharacterStats { get; private set; }

        public override void Init()
        {
            CharacterStats = new List<StatEntry>();

            foreach (var entry in CharacterStatsOnZeroLevel)
            {
                CharacterStats.Add(new StatEntry
                {
                    Stat = entry.Stat,
                    Value = entry.Value
                });
            }
        }

        public override void SaveProgress()
        {

        }

        public void OnLevelUP(Int32 level)
        {
            for (Int32 i = 0; i < CharacterStats.Count; i++)
            {
                if (MainStats.Contains(CharacterStats[i].Stat))
                    CharacterStats[i].Value += level * BaseStatIncrease * 2;
                else
                    CharacterStats[i].Value += level * BaseStatIncrease;
            }
        }

        public override void CheckForUpdate()
        {

        }
    }
}