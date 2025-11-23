using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PopUp
{
    public class CharacterMainInfoView : MonoBehaviour
    {
        [Header("Аватар и описание")]
        [SerializeField, ShowInInspector]
        private Image _avatarImage;

        [SerializeField, ShowInInspector]
        private TMP_Text _idText;

        [SerializeField, ShowInInspector]
        private TMP_Text _levelText;

        [SerializeField, ShowInInspector]
        private TMP_Text _descriptionText;

        public void SetAvatarImage(Sprite image)
        {
            _avatarImage.sprite = image;
        }

        public void SetPlayerID(String idString)
        {
            _idText.text = idString;
        }

        public void SetLevel(String levelString)
        {
            _levelText.text = levelString;
        }

        public void SetDescription(String descriptionString)
        {
            _descriptionText.text = descriptionString;
        }

        public void UpdateLevel(String levelString)
        {
            SetLevel(levelString);
        }
    }
}
