using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

namespace PopUp
{
    [CreateAssetMenu(fileName = "NewCharacter", menuName = "Character")]
    public class CharacterModel : ScriptableObject
    {
        public event Action<Int32, List<StatEntry>> LevelUpdateEvent;
        public event Action<Int32, Int32> XPUpdateEvent;

        public event Action CanLevelUpEvent;
        public event Action CanNotLevelUpEvent;

        [Header("Аватар")]
        public Sprite Avatar;

        [Header("Описание")]
        public String Description;

        [Header("Начальный уровень")]
        public Int32 InitLevel;

        [Header("Начальный опыт")]
        public Int32 InitXP;

        [Header("Опыт для LevelUP")]
        public Int32 RequiredXP;

        [Header("Основной стат")]
        public List<StatsType> MainStats;

        [Header("Базовый прирост за уровень")]
        public Int32 BaseStatIncrease;

        [Header("Статы на 0 уровне")]
        [SerializeField, TableList]
        public List<StatEntry> CharacterStatsOnZeroLevel;

        public Int32 CurrentLevel { get; private set; }
        public Int32 CurrentXP { get; private set; }
        public List<StatEntry> CharacterStats { get; private set; }

        public void OnEnable()
        {
            CurrentLevel = 0;
            CurrentXP = InitXP;

            CharacterStats = new List<StatEntry>();

            foreach (var entry in CharacterStatsOnZeroLevel)
            {
                CharacterStats.Add(new StatEntry
                {
                    Stat = entry.Stat,
                    Value = entry.Value
                });
            }

            LevelUp(InitLevel);
        }

        public void AddXP(Int32 gain)
        {
            CurrentXP += gain;

            XPUpdateEvent?.Invoke(CurrentXP, RequiredXP);

            if (IsCanLevelUp())
                CanLevelUpEvent?.Invoke();
            else
                CanNotLevelUpEvent?.Invoke();
        }

        public Boolean IsCanLevelUp()
        {
            return CurrentXP >= RequiredXP;
        }

        public void TryLevelUp()
        {
            Int32 levels = CurrentXP / RequiredXP;

            for (Int32 i = 0; i < CharacterStats.Count; i++)
            {
                if (MainStats.Contains(CharacterStats[i].Stat))
                    CharacterStats[i].Value += levels * BaseStatIncrease * 2;
                else
                    CharacterStats[i].Value += levels * BaseStatIncrease;
            }

            CurrentXP %= RequiredXP;
            CurrentLevel += levels;

            XPUpdateEvent?.Invoke(CurrentXP, RequiredXP);
            LevelUpdateEvent?.Invoke(CurrentLevel, CharacterStats);
            CanNotLevelUpEvent?.Invoke();
        }

        private void LevelUp(Int32 levels)
        {
            for (Int32 i = 0; i < CharacterStats.Count; i++)
            {
                if (MainStats.Contains(CharacterStats[i].Stat))
                    CharacterStats[i].Value += levels * BaseStatIncrease * 2;
                else
                    CharacterStats[i].Value += levels * BaseStatIncrease;
            }

            CurrentLevel += levels;
        }
    }
}
