using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SaveGameHW
{
    public class GameRepositoryService : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _savedPrefabs;

        private Dictionary<string, GameObject> _prefabMap;

        private void OnEnable()
        {
            _prefabMap = _savedPrefabs.ToDictionary(p => p.name, p => p);
        }

        public TResult Spawn<TResult, TData>(TData data)
            where TResult : MonoBehaviour, ISaveable<TData>
            where TData : class
        {
            var prefabName = GetPrefabName(data);

            if(!_prefabMap.TryGetValue(prefabName, out var prefab))
            {
                throw new Exception("Хрень с получением префаба с мапа");
            }

            var spawnedObject = Instantiate(prefab);
            var component = spawnedObject.AddComponent<TResult>();
            component.LoadData(data);
            return component;
        }

        private String GetPrefabName<TData>(TData data)
        {
            var field = typeof(TData).GetField("PrefabName");
            if(field != null && field.FieldType == typeof(String))
            {
                return (String) field.GetValue(data);
            }

            var prop = typeof(TData).GetProperty("PrefabName");
            if (prop != null && prop.PropertyType == typeof(string))
            {
                return (string)prop.GetValue(data);
            }

            return null;

        }
    }

}

