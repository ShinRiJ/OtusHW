using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;

namespace PopUp
{
    public class StatView : MonoBehaviour
    {
        [SerializeField, ShowInInspector]
        private TMP_Text _outputText;

        [SerializeField]
        private String _stringKey;

        public StatsType Type;

        public void SetStatValue(Int32 value)
        {
            _outputText.text = $"{_stringKey}: {value.ToString()}";
        }
    }
}
