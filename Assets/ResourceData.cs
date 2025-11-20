using System;
using UnityEngine;
using Homeworks.SaveLoad;

namespace SaveGameHW
{
    [Serializable]
    public class ResourceData
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
        public ResourceType ResourceType;
        public int RemainingCount;

        public ResourceData(Transform transform, string prefabName, ResourceType resourceType, int remainingCount)
        {
            PositionAndRotationWorld = new TransformData
            {
                Position = transform.position,
                Rotation = transform.rotation,
                ParentPath = transform.parent ? transform.parent.name : null
            };

            PrefabName = prefabName;
            ResourceType = resourceType;
            RemainingCount = remainingCount;
        }
    }
}
