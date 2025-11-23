using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PopUp
{
    public class CharacterXPBarView : MonoBehaviour
    {
        [Header("XP бар")]
        [SerializeField, ShowInInspector]
        private Image _XPBarNotCompletedImage;

        [SerializeField, ShowInInspector]
        private Image _XPBarCompletedImage;

        [SerializeField, ShowInInspector]
        private TMP_Text _XPBarText;

        public void SetXPBarString(String xpString)
        {
            _XPBarText.text = xpString;
        }

        public void SetBarAmount(Single value)
        {
            _XPBarNotCompletedImage.fillAmount = value;
        }

        public void SetIncompletedXPBar()
        {
            _XPBarNotCompletedImage.gameObject.SetActive(true);
            _XPBarCompletedImage.gameObject.SetActive(false);
        }

        public void SetCompletedXPBar()
        {
            _XPBarNotCompletedImage.gameObject.SetActive(false);
            _XPBarCompletedImage.gameObject.SetActive(true);
        }
    }
}
