using SaveGameHW;
using System;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    [Serializable]
    public sealed class ResourceObject :  MonoBehaviour, ISaveable<ResourceData>
    {
        [SerializeField] private String _prefabName;
        [SerializeField] private Int32 _remainingCount;
        [SerializeField] private ResourceType _resourceType;

        public String PrefabName { get => _prefabName; }
        public Int32 RemainingCount { get => _remainingCount; }
        public ResourceType ResourceType { get => _resourceType; }

        public SaveEntity GetSaveEntity()
        {
            return new SaveEntity(nameof(ResourceData), JsonUtility.ToJson(SaveData()));
        }

        public void LoadData(ResourceData data)
        {
            _prefabName = data.PrefabName;
            _remainingCount = data.RemainingCount;
            _resourceType = data.ResourceType;

            transform.position = data.PositionAndRotationWorld.Position;
            transform.rotation= data.PositionAndRotationWorld.Rotation;

            if (!string.IsNullOrEmpty(data.PositionAndRotationWorld.ParentPath))
            {
                var parentObj = GameObject.Find(data.PositionAndRotationWorld.ParentPath);
                if (parentObj != null)
                    transform.parent = parentObj.transform;
            }


        }

        public ResourceData SaveData()
        {
            return new ResourceData(transform, _prefabName, _resourceType, _remainingCount);
        }
    }
}