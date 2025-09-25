using System;

namespace PopUp
{
    internal class CharacterViewModel : IDisposable
    {
        public event Action OnCloseEvent;

        private readonly CharacterModel _characterModel;
        private readonly CharacterInfoView _characterInfoView;

        public CharacterViewModel(CharacterModel characterModel, CharacterInfoView characterInfoView, Int32 playerID)
        {
            _characterModel = characterModel;
            _characterInfoView = characterInfoView;

            Bind();
            InitializeView();

            _characterInfoView.SetPlayerID(playerID.ToString());
        }

        private void Bind()
        {
            _characterModel.CanLevelUpEvent += _characterInfoView.SetLevelUpEnable;
            _characterModel.CanNotLevelUpEvent += _characterInfoView.SetLevelUpDisable;
            _characterModel.XPUpdateEvent += _characterInfoView.SetXPBarValue;
            _characterModel.LevelUpdateEvent += _characterInfoView.UpdateLevelAndStats;

            _characterInfoView.OnLevelUpEvent += _characterModel.TryLevelUp;
            _characterInfoView.OnCloseEvent += OnClose;
        }

        private void OnClose()
        {
            OnCloseEvent?.Invoke();
        }

        private void InitializeView()
        {
            _characterModel.AddXP(0);

            _characterInfoView.SetLevel(_characterModel.CurrentLevel);
            _characterInfoView.StatsUpdate(_characterModel.CharacterStats);
            _characterInfoView.SetDescription(_characterModel.Description);
            _characterInfoView.SetAvatarImage(_characterModel.Avatar);
        }

        public void Dispose()
        {
            _characterModel.CanLevelUpEvent -= _characterInfoView.SetLevelUpEnable;
            _characterModel.CanNotLevelUpEvent -= _characterInfoView.SetLevelUpDisable;
            _characterModel.XPUpdateEvent -= _characterInfoView.SetXPBarValue;
            _characterModel.LevelUpdateEvent -= _characterInfoView.UpdateLevelAndStats;

            _characterInfoView.OnLevelUpEvent -= _characterModel.TryLevelUp;
            _characterInfoView.OnCloseEvent -= OnCloseEvent;
        }
    }
}
