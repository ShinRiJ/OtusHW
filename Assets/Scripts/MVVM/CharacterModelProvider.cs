using System;
using System.Collections.Generic;
using UnityEngine;

namespace PopUp
{
    [CreateAssetMenu(fileName = "NewProvider", menuName = "CharacterModelProvider")]
    public class CharacterModelProvider : ScriptableObject, IDisposable
    {
        [SerializeField] private List<BaseModel> _baseModels;

        public void Dispose()
        {
            ModelsSave();
        }

        public BaseModel GetModelByType<T>() where T : BaseModel
        {
            if (_baseModels != null)
            {
                for (Int32 i = 0; i < _baseModels.Count; i++)
                {
                    if (_baseModels[i] is T model)
                    {
                        return model;
                    }
                }
            }

            throw new Exception("ѕроверить список моделей провайдера!!!");
        }

        public void ModelsInit()
        {
            for (Int32 i = 0; i < _baseModels.Count; i++)
            {
                _baseModels[i].Init();
            }
        }

        public void ModelsSave()
        {
            for (Int32 i = 0; i < _baseModels.Count; i++)
            {
                _baseModels[i].SaveProgress();
            }
        }

        public void ModelsUpdate()
        {
            for (Int32 i = 0; i < _baseModels.Count; i++)
            {
                _baseModels[i].CheckForUpdate();
            }
        }


    }
}
