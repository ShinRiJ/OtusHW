using System;
using UnityEngine;

namespace SaveGameHW
{
    [Serializable]
    public class UnitData
    {
        [Serializable]
        public struct TransformData
        {
            public Vector3 Position;
            public Quaternion Rotation;
            public string ParentPath;
        }

        public TransformData PositionAndRotationWorld;

        public string PrefabName;
        public int HitPoints;
        public int Speed;
        public int Damage;

        public UnitData(Transform transform, int hp, int speed, int damage, string prefabName)
        {
            PositionAndRotationWorld = new TransformData
            {
                Position = transform.position,
                Rotation = transform.rotation,
                ParentPath = transform.parent ? transform.parent.name : null
            };

            PrefabName = prefabName;
            HitPoints = hp;
            Speed = speed;
            Damage = damage;
        }
    }
}
