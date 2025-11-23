using System;
using UnityEngine;

namespace PopUp
{
    [CreateAssetMenu(fileName = "NewCharacterMainInfoModel", menuName = "CharacterMainInfoModel")]
    public class CharacterMainInfoModel : BaseModel
    {
        public event Action<Int32> ModelNeedStatsUpdateEvent;

        [Header("Аватар")]
        public Sprite Avatar;

        [Header("Описание")]
        public String Description;

        [Header("Начальный уровень")]
        public Int32 InitLevel;

        public Int32 CurrentLevel { get; private set; }

        public override void Init()
        {
            CurrentLevel = InitLevel;
        }

        public override void SaveProgress()
        {
            InitLevel = CurrentLevel;
        }

        public void LevelUP(Int32 levels)
        {
            CurrentLevel = CurrentLevel + levels;
        }

        public override void CheckForUpdate()
        {
            if (InitLevel > 1)
                ModelNeedStatsUpdateEvent?.Invoke(InitLevel - 1);
        }

        public void ModelClearEvent()
        {
            ModelNeedStatsUpdateEvent = null;
        }
    }
}
