using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PopUp
{
    public class CharacterViewProvider : MonoBehaviour
    {
        [SerializeField] private CharacterMainInfoView _characterMainInfoView;
        [SerializeField] private CharacterXPBarView _characterInfoXPBarView;
        [SerializeField] private CharacterStatsView _characterStatsView;
        [SerializeField] private CharacterButtonsView _characterButtonsView;

        public CharacterMainInfoView CharacterMainInfoView
        {
            get { return _characterMainInfoView; }
        }

        public CharacterXPBarView CharacterInfoXPBarView
        {
            get { return _characterInfoXPBarView; }
        }

        public CharacterStatsView CharacterStatsView
        {
            get { return _characterStatsView; }
        }

        public CharacterButtonsView CharacterButtonsView
        {
            get { return _characterButtonsView; }
        }
    }
}
