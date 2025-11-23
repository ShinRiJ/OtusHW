using System;

namespace PopUp
{
    public class CharacterMainInfoViewModel : IViewModel
    {
        public event Action<Int32> NeedStatsUpdateEvent;

        private CharacterMainInfoModel _characterMainInfoModel;
        private CharacterMainInfoView _characterMainInfoView;

        public CharacterMainInfoViewModel(CharacterMainInfoModel characterMainInfoModel, CharacterMainInfoView characterMainInfoView, Int32 playerID)
        {
            _characterMainInfoModel = characterMainInfoModel;
            _characterMainInfoView = characterMainInfoView;

            _characterMainInfoView.SetPlayerID(GetIdString(playerID));
            Init();
        }

        public void Init()
        {
            _characterMainInfoModel.ModelNeedStatsUpdateEvent += OnLevelNotFirst;

            _characterMainInfoView.SetAvatarImage(_characterMainInfoModel.Avatar);
            _characterMainInfoView.SetDescription(_characterMainInfoModel.Description);
            _characterMainInfoView.SetLevel(GetLevelString(_characterMainInfoModel.InitLevel));
        }

        public void OnLevelUP(Int32 levels)
        {
            _characterMainInfoModel.LevelUP(levels);
            _characterMainInfoView.SetLevel(GetLevelString(_characterMainInfoModel.CurrentLevel));
        }

        private String GetLevelString(Int32 level)
        {
            return $"Level: {level.ToString()}";
        }

        private String GetIdString(Int32 id)
        {
            return $"@USER{id.ToString()}";
        }

        public void Dispose()
        {
            _characterMainInfoModel.ModelClearEvent();

            NeedStatsUpdateEvent = null;

            _characterMainInfoModel = null;
            _characterMainInfoView = null;
        }

        private void OnLevelNotFirst(Int32 diff)
        {
            NeedStatsUpdateEvent?.Invoke(diff);
        }
    }
}