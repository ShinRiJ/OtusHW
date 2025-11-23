using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PopUp
{
    [CreateAssetMenu(fileName = "NewCharacterXPModel", menuName = "CharacterXPModel")]
    public class CharacterXPModel : BaseModel
    {
        public event Action ModelCanLevelUP;
        public event Action ModelCanNotLevelUP;

        public event Action<Int32> ModelLevelUPEvent;
        public event Action<Int32> ModelXPChagedEvent;

        [Header("Начальный опыт")]
        public Int32 InitXP;

        [Header("Опыт для LevelUP")]
        public Int32 RequiredXP;

        public Int32 CurrentXP { get; private set; }

        public override void Init()
        {
            CurrentXP = InitXP;
        }

        public override void SaveProgress()
        {
            InitXP = CurrentXP;
        }

        public void GainXP(Int32 gain)
        {
            CurrentXP = CurrentXP + gain;

            if (IsCanLevelUp())
                ModelCanLevelUP?.Invoke();
            else
                ModelCanNotLevelUP?.Invoke();

            ModelXPChagedEvent?.Invoke(CurrentXP);
        }

        private Boolean IsCanLevelUp()
        {
            return CurrentXP >= RequiredXP;
        }

        public void TryLevelUp()
        {
            Int32 levels = CurrentXP / RequiredXP;

            CurrentXP %= RequiredXP;

            if (levels > 0)
            {
                ModelLevelUPEvent?.Invoke(levels);

                if (IsCanLevelUp())
                    ModelCanLevelUP?.Invoke();
                else
                    ModelCanNotLevelUP?.Invoke();

                ModelXPChagedEvent?.Invoke(CurrentXP);
            }
        }

        public override void CheckForUpdate()
        {
            //TryLevelUp();
        }

        public void ModelClearEvent()
        {
            ModelCanLevelUP = null;
            ModelCanNotLevelUP = null;

            ModelLevelUPEvent = null;
            ModelXPChagedEvent = null;
        }
    }
}
