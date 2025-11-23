using System;
using UnityEngine;

namespace PopUp
{
    public class CharacterXPViewModel : IViewModel
    {
        private event Action TryLevelUP;

        public event Action CanLevelUP;
        public event Action CanNotLevelUP;

        public event Action<Int32> LevelUPEvent;

        private CharacterXPModel _characterXPModel;
        private CharacterXPBarView _characterXPBarView;

        public CharacterXPViewModel(CharacterXPModel characterXPModel, CharacterXPBarView characterXPBarView)
        {
            _characterXPModel = characterXPModel;
            _characterXPBarView = characterXPBarView;

            Init();
        }

        public void Init()
        {
            TryLevelUP += _characterXPModel.TryLevelUp;

            _characterXPModel.ModelXPChagedEvent += OnModelXPChanged;
            _characterXPModel.ModelLevelUPEvent += OnModelLevelUp;

            _characterXPModel.ModelCanLevelUP += OnModelCanLevelUP;
            _characterXPModel.ModelCanNotLevelUP += OnModelCanNotLevelUP;

            OnModelXPChanged(_characterXPModel.CurrentXP);
        }

        public void Dispose()
        {
            _characterXPModel.ModelClearEvent();

            _characterXPModel = null;
            _characterXPBarView = null;

            TryLevelUP = null;
            CanLevelUP = null;
            CanNotLevelUP = null;
            LevelUPEvent = null;
        }

        private void OnModelLevelUp(Int32 levelAdded)
        {
            LevelUPEvent?.Invoke(levelAdded);
        }

        private void OnModelXPChanged(Int32 currentXP)
        {
            Single barValue = Mathf.Clamp01((Single)currentXP / _characterXPModel.RequiredXP);

            _characterXPBarView.SetBarAmount(barValue);
            _characterXPBarView.SetXPBarString(GetXPString(currentXP, _characterXPModel.RequiredXP));
        }

        private void OnModelCanLevelUP()
        {
            _characterXPBarView.SetCompletedXPBar();
        }

        private void OnModelCanNotLevelUP()
        {
            _characterXPBarView.SetIncompletedXPBar();
        }

        private String GetXPString(Int32 currentValue, Int32 maxValue)
        {
            return $"XP: {currentValue}/{maxValue}";
        }

        public void TryExternLevelUP()
        {
            TryLevelUP?.Invoke();
        }
    }
}