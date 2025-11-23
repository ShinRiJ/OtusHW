using System;

namespace PopUp
{
    public class CharacterStatsViewModel : IViewModel
    {
        CharacterStatsView _characterStatsView;
        CharacterStatsModel _characterStatsModel;

        public CharacterStatsViewModel(CharacterStatsModel characterStatsModel, CharacterStatsView characterStatsView)
        {
            _characterStatsView = characterStatsView;
            _characterStatsModel = characterStatsModel;

            Init();
        }

        public void Init()
        {
            SetStats();
        }

        private void SetStats()
        {
            for (Int32 i = 0; i < _characterStatsModel.CharacterStats.Count; i++)
            {
                _characterStatsView.SetStatValue(
                    _characterStatsModel.CharacterStats[i].Stat,
                    _characterStatsModel.CharacterStats[i].Value
                );
            }
        }

        public void OnLevelUP(Int32 level)
        {
            _characterStatsModel.OnLevelUP(level);

            SetStats();
        }

        public void Dispose()
        {
            _characterStatsView = null;
            _characterStatsModel = null;
        }
    }
}