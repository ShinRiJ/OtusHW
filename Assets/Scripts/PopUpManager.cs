using Newtonsoft.Json.Linq;
using PopUp;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using Zenject.SpaceFighter;
using static PopUp.CharacterStatsViewModel;

namespace PopUp
{
    public class PopUpManager : MonoBehaviour
    {
        [SerializeField, ShowInInspector]
        private Int32 _playerID;

        private Int32 _currentCharID;

        [SerializeField, ShowInInspector]
        private GameObject _characterListPrefab;

        [SerializeField, ShowInInspector]
        private List<CharacterModelProvider> _characterModelProviders;

        private GameObject _popUpObject;

        private CharacterViewProvider _characterViewProvider;
        private CharacterModelProvider _currentCharacterModelProvider;

        private CharacterXPModel _currentCharacterXPFastLinkDebug;

        private List<IViewModel> _currentCharacterViewModelList;
        
        private void Start()
        {
            _popUpObject = Instantiate(_characterListPrefab);
            _popUpObject.transform.SetParent(transform, false);

            _currentCharID = 0;

            _characterViewProvider = _popUpObject.GetComponent<CharacterViewProvider>();

            _currentCharacterViewModelList = new List<IViewModel>();

            CharacterSwitch(_characterModelProviders[_currentCharID]);
        }

        private void OnDestroy()
        {
            if (_currentCharacterModelProvider != null)
            {
                _currentCharacterModelProvider.ModelsSave();
            }
        }

        private void CharacterSwitch(CharacterModelProvider characterModel)
        {
            if(_currentCharacterModelProvider != null)
            {
                _currentCharacterModelProvider.ModelsSave();
            }

            _currentCharacterModelProvider = characterModel;
            _currentCharacterModelProvider.ModelsInit();

            if (_currentCharacterViewModelList != null)
            {
                for(Int32 i = 0; i < _currentCharacterViewModelList.Count; i++)
                {
                    _currentCharacterViewModelList[i].Dispose();
                }

                _currentCharacterViewModelList.Clear();
            }

            CharacterMainInfoViewModel characterMainInfoViewModel = null;
            CharacterXPViewModel characteXPViewModel = null;
            CharacterStatsViewModel characterStatsViewModel = null;


            if (_currentCharacterModelProvider.GetModelByType<CharacterMainInfoModel>() is CharacterMainInfoModel characterMainInfoModel)
            {
                characterMainInfoViewModel = new CharacterMainInfoViewModel(
                    characterMainInfoModel,
                    _characterViewProvider.CharacterMainInfoView,
                    _playerID
                );

                _currentCharacterViewModelList.Add(characterMainInfoViewModel);
            }

            CharacterXPModel characterXPModel = _currentCharacterModelProvider.GetModelByType<CharacterXPModel>() as CharacterXPModel;
            _currentCharacterXPFastLinkDebug = characterXPModel;

            if (characterXPModel != null)
            {
                characteXPViewModel = new CharacterXPViewModel(
                    characterXPModel,
                    _characterViewProvider.CharacterInfoXPBarView
                );

                _currentCharacterViewModelList.Add(characteXPViewModel);
            }

            if (_currentCharacterModelProvider.GetModelByType<CharacterStatsModel>() is CharacterStatsModel characterStatsModel)
            {
                characterStatsViewModel = new CharacterStatsViewModel(
                    characterStatsModel,
                    _characterViewProvider.CharacterStatsView
                );

                _currentCharacterViewModelList.Add(characterStatsViewModel);
            }

            CharacterButtonEventHandler characterButtonEventHandler = new CharacterButtonEventHandler(_characterViewProvider.CharacterButtonsView);

            if(characterMainInfoViewModel == null || characteXPViewModel == null ||
               characterStatsViewModel == null || characterButtonEventHandler == null)
            {
                throw new ArgumentNullException("Проблема при создании ViewModel");
            }

            characteXPViewModel.LevelUPEvent += characterMainInfoViewModel.OnLevelUP;
            characteXPViewModel.LevelUPEvent += characterStatsViewModel.OnLevelUP;
            characterMainInfoViewModel.NeedStatsUpdateEvent += characterStatsViewModel.OnLevelUP;

            characteXPViewModel.CanLevelUP += characterButtonEventHandler.OnCanLevelUP;
            characteXPViewModel.CanNotLevelUP += characterButtonEventHandler.OnCanNotLevelUP;

            characterButtonEventHandler.OnCloseEvent += OnClose;
            characterButtonEventHandler.OnLevelUpTryEvent += characteXPViewModel.TryExternLevelUP;

            _currentCharacterModelProvider?.ModelsUpdate();
        }

        public void MoveNext()
        {
            _currentCharID = (_currentCharID + 1) % _characterModelProviders.Count;
            CharacterSwitch(_characterModelProviders[_currentCharID]);
        }

        public void MoveBack()
        {
            _currentCharID = (_currentCharID - 1 + +_characterModelProviders.Count) % _characterModelProviders.Count;
            CharacterSwitch(_characterModelProviders[_currentCharID]);
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
            _currentCharacterXPFastLinkDebug.GainXP(value);
        }
    }
}
