using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

namespace PopUp
{
    public class CharacterButtonsView : MonoBehaviour
    {
        [Header("Кнопки")]
        [SerializeField, ShowInInspector]
        public Button CloseButton;

        [SerializeField, ShowInInspector]
        public Button LevelUPButtonActive;

        [SerializeField, ShowInInspector]
        public Image LevelUPButtonInactive;
    }
}
