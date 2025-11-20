using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace SaveGameHW
{
    public class SaveService
    {
        private readonly String _savePath;
        private readonly Dictionary<String, IRestorer> _restorers;

        public SaveService()
        {
            _savePath = Path.Combine(Application.persistentDataPath, "HomeWorkSave.json");
        }

        public void SaveScene()
        {
            var saveData = new SaveData();

            var maybeSaveables = UnityEngine.Object.FindObjectsOfType<MonoBehaviour>(true);

            foreach (var maybeSaveable in maybeSaveables)
            {
                var type = maybeSaveable.GetType();

                foreach(var interfaceIter in type.GetInterfaces())
                {
                    if(interfaceIter.IsGenericType && interfaceIter.GetGenericTypeDefinition() == typeof(ISaveable<>))
                    {
                        var method = interfaceIter.GetMethod("SaveData");
                        var data = method.Invoke(maybeSaveable, null);

                        var json = JsonUtility.ToJson(data);

                        saveData.Entities.Add(new SaveEntity(type.Name.ToString(), json));
                    }
                }
            }

            var encoded = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(saveData, true)));
            File.WriteAllText(_savePath, encoded);

            Debug.Log($"[SaveService] Игра сохранена в {_savePath}");
        }

        public SaveData LoadScene()
        {
            if (!File.Exists(_savePath))
            {
                Debug.LogWarning("[SaveService] Ошибка попытки загрузки !!!");
                return new SaveData();
            }

            var encoded = File.ReadAllText(_savePath);
            var json = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
            return JsonUtility.FromJson<SaveData>(json);
        }
    }
}
