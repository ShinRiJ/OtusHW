using SaveGameHW;
using System;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    [Serializable]
    public sealed class UnitObject : MonoBehaviour, ISaveable<UnitData>
    {
        [SerializeField] private String _prefabName;

        [SerializeField] private Int32 _hitPoints;
        [SerializeField] private Int32 _speed;
        [SerializeField] private Int32 _damage;

        public String PrefabName { get => _prefabName; }
        public Int32 HitPoints { get => _hitPoints; }
        public Int32 Speed { get => _speed; }
        public Int32 Damage { get => _damage; }

        public void LoadData(UnitData data)
        {
            _hitPoints = data.HitPoints;
            _speed = data.Speed;
            _damage = data.Damage;
            _prefabName = data.PrefabName;

            transform.position = data.PositionAndRotationWorld.Position;
            transform.rotation = data.PositionAndRotationWorld.Rotation;

            if (!string.IsNullOrEmpty(data.PositionAndRotationWorld.ParentPath))
            {
                var parentObj = GameObject.Find(data.PositionAndRotationWorld.ParentPath);
                if (parentObj != null)
                    transform.parent = parentObj.transform;
            }
        }

        public UnitData SaveData()
        {
            return new UnitData(transform, _hitPoints, _speed, _damage, _prefabName);    
        }

        public SaveEntity GetSaveEntity()
        {
            return new SaveEntity(nameof(ResourceData), JsonUtility.ToJson(SaveData()));
        }
    }
}