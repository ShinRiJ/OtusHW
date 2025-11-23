using System;

namespace PopUp
{
    public class CharacterButtonEventHandler : IViewModel
    {
        public event Action OnCloseEvent;
        public event Action OnLevelUpTryEvent;

        private CharacterButtonsView _characterButtonsView;

        public CharacterButtonEventHandler(CharacterButtonsView characterButtonsView)
        {
            _characterButtonsView = characterButtonsView;

            Init();
        }

        public void OnCanLevelUP()
        {
            _characterButtonsView.LevelUPButtonActive.gameObject.SetActive(true);
            _characterButtonsView.LevelUPButtonInactive.gameObject.SetActive(false);
        }

        public void OnCanNotLevelUP()
        {
            _characterButtonsView.LevelUPButtonActive.gameObject.SetActive(false);
            _characterButtonsView.LevelUPButtonInactive.gameObject.SetActive(true);
        }
        public void OnCloseButton()
        {
            OnCloseEvent?.Invoke();
        }

        public void OnLevelUPButton()
        {
            OnLevelUpTryEvent?.Invoke();
        }

        public void Init()
        {
            _characterButtonsView.CloseButton.onClick.AddListener(OnCloseButton);
            _characterButtonsView.LevelUPButtonActive.onClick.AddListener(OnLevelUPButton);
        }

        public void Dispose()
        {
            _characterButtonsView.CloseButton.onClick.RemoveAllListeners();
            _characterButtonsView.LevelUPButtonActive.onClick.RemoveAllListeners();

            _characterButtonsView = null;
        }
    }
}