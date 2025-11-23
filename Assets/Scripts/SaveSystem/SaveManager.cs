using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace SaveGameHW
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private GameRepositoryService _repo;

        private SaveService _saveService;
        private Dictionary<string, IRestorer> _restorers;

        private void Awake()
        {
            _saveService = new SaveService();
            _restorers = BuildRestorers();
        }

        private void Start()
        {
            //LoadGame();
        }

        public void ClearScene()
        {
            var candidates = FindObjectsOfType<MonoBehaviour>()
                .Where(mb => mb.GetType().GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISaveable<>)))
                        .ToList();

            foreach (var mb in candidates)
            {
                Destroy(mb.gameObject);
            }
        }

        public void SaveGame()
        {
            _saveService.SaveScene();
        }

        public void LoadGame()
        {
            ClearScene();

            var saveData = _saveService.LoadScene();

            foreach(var entity in saveData.Entities)
            {
                
                if(_restorers.TryGetValue(entity.Type.ToString(), out var restorer))
                {
                    restorer.Restore(entity.Json, _repo);
                }
                else
                {
                    Debug.LogWarning($"Какая-то хрень с ресторером для {entity.Type}");
                }
            }
        }

        private Dictionary<string, IRestorer> BuildRestorers()
        {
            var restorers = new Dictionary<string, IRestorer>();
            var assembly = Assembly.GetExecutingAssembly();

            var candidates = assembly.GetTypes().Where(t => typeof(MonoBehaviour).IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var candidate in candidates)
            {
                foreach (var interfaceIter in candidate.GetInterfaces())
                {
                    if (interfaceIter.IsGenericType && interfaceIter.GetGenericTypeDefinition() == typeof(ISaveable<>))
                    {
                        var dataType = interfaceIter.GetGenericArguments()[0];
                        var restorerType = typeof(Restorer<,>).MakeGenericType(candidate, dataType);
                        var restorer = (IRestorer)System.Activator.CreateInstance(restorerType);
                        restorers[candidate.Name] = restorer;
                    }
                }
            }

            return restorers;
        }

    }
}
