using PopUp;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PopUp
{
    public class PopUpManager : MonoBehaviour
    {
        [SerializeField, ShowInInspector]
        private Int32 _playerID;

        [SerializeField, ShowInInspector]
        private GameObject _characterInfoViewPrefab;

        [SerializeField, ShowInInspector]
        private List<CharacterModel> _characterModels;

        private CharacterModel _currentCharacterModel;
        private CharacterInfoView _characterInfoView;
        private CharacterViewModel _characterViewModel;

        private GameObject _popUpObject;

        private Int32 _currentCharID;

        private void Start()
        {
            _popUpObject = Instantiate(_characterInfoViewPrefab);
            _popUpObject.transform.SetParent(transform, false);

            _characterInfoView = _popUpObject.GetComponent<CharacterInfoView>();

            _currentCharID = 0;
            CharacterSwitch(_characterModels[_currentCharID]);
        }

        private void CharacterSwitch(CharacterModel characterModel)
        {
            if(_characterViewModel != null)
                _characterViewModel.Dispose();

            _characterViewModel = new CharacterViewModel(characterModel, _characterInfoView, _playerID);
            _characterViewModel.OnCloseEvent += OnClose;

            _currentCharacterModel = characterModel;
        }

        public void MoveNext()
        {
            _currentCharID = (_currentCharID + 1) % _characterModels.Count;
            CharacterSwitch(_characterModels[_currentCharID]);
        }

        public void MoveBack()
        {
            _currentCharID = (_currentCharID - 1 + +_characterModels.Count) % _characterModels.Count;
            CharacterSwitch(_characterModels[_currentCharID]);
        }

        private void OnClose()
        {
            _popUpObject.SetActive(false);
        }

        public void OnOpen()
        {
            _popUpObject.SetActive(true);
        }

        public void AddXPToCurrentCharacter(Int32 value)
        {
            _currentCharacterModel.AddXP(value);
        }
    }
}
