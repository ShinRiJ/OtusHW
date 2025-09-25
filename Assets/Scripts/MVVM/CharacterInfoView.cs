using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PopUp
{
    public class CharacterInfoView : MonoBehaviour
    {
        public event Action OnCloseEvent;
        public event Action OnLevelUpEvent;

        [Header("јватар и описание")]
        [SerializeField, ShowInInspector]
        private Image _avatarImage;

        [SerializeField, ShowInInspector]
        private TMP_Text _idText;

        [SerializeField, ShowInInspector]
        private TMP_Text _levelText;

        [SerializeField, ShowInInspector] 
        private TMP_Text _descriptionText;

        [Header("XP бар")]
        [SerializeField, ShowInInspector]
        private Image _XPBarNotCompletedImage;

        [SerializeField, ShowInInspector]
        private Image _XPBarCompletedImage;

        [SerializeField, ShowInInspector]
        private TMP_Text _XPBarText;

        [Header("XP бар")]
        [SerializeField, ShowInInspector]
        private List<StatView> _statsVievList;

        [Header(" нопки")]
        [SerializeField, ShowInInspector]
        private Button _closeButton;

        [SerializeField, ShowInInspector]
        private Button _levelUPButtonActive;

        [SerializeField, ShowInInspector]
        private Image _levelUPButtonInactive;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnClose);
            _levelUPButtonActive.onClick.AddListener(OnLevelUp);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnClose);
            _levelUPButtonActive.onClick.RemoveListener(OnLevelUp);
        }

        public void SetAvatarImage(Sprite image)
        {
            _avatarImage.sprite = image;
        }

        public void UpdateLevelAndStats(Int32 level, List<StatEntry> statEntries)
        {
            SetLevel(level);
            StatsUpdate(statEntries);
        }

        public void SetPlayerID(String value)
        {
            _idText.text = $"@USER{value}";
        }

        public void SetLevel(Int32 level)
        {
            _levelText.text = $"Level: {level.ToString()}";
        }

        public void SetDescription(String description)
        {
            _descriptionText.text = description;
        }

        private void SetIncompletedXPBar()
        {
            _XPBarNotCompletedImage.gameObject.SetActive(true);
            _XPBarCompletedImage.gameObject.SetActive(false);
        }

        private void SetCompletedXPBar()
        {
            _XPBarNotCompletedImage.gameObject.SetActive(false);
            _XPBarCompletedImage.gameObject.SetActive(true);
        }

        public void SetXPBarValue(Int32 currentValue, Int32 maxValue)
        {
            _XPBarNotCompletedImage.fillAmount = Mathf.Clamp01((Single) currentValue / maxValue);
            _XPBarText.text = $"XP: {currentValue}/{maxValue}";
        }

        public void StatsUpdate(List<StatEntry> statEntries)
        {
            for(Int32 i = 0; i < statEntries.Count; i++)
            {
                SetStatValue(statEntries[i].Stat, statEntries[i].Value);
            }
        }

        private void SetStatValue(StatsType statsType, Int32 value)
        {
            for(Int32 i = 0; i < _statsVievList.Count; i++)
            {
                if (_statsVievList[i].Type == statsType)
                {
                    _statsVievList[i].SetStatValue(value);
                    return;
                }
            }

            throw new Exception("ќшибка при обновлении стата");
        }

        public void SetLevelUpEnable()
        {
            SetCompletedXPBar();

            _levelUPButtonActive.gameObject.SetActive(true);
            _levelUPButtonInactive.gameObject.SetActive(false);
        }

        public void SetLevelUpDisable()
        {
            SetIncompletedXPBar();

            _levelUPButtonActive.gameObject.SetActive(false);
            _levelUPButtonInactive.gameObject.SetActive(true);
        }

        private void OnClose()
        {
            OnCloseEvent?.Invoke();
        }

        private void OnLevelUp()
        {
            OnLevelUpEvent?.Invoke();
        }
    }
}
