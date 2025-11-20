using System;
using UnityEngine;

namespace SaveGameHW
{
    public interface IRestorer
    {
        void Restore(string json, GameRepositoryService repositoryService);
    }

    public class Restorer<TResult, TData> : IRestorer
        where TResult : MonoBehaviour, ISaveable<TData>
        where TData : class
    {
        public void Restore(string json, GameRepositoryService repositoryService)
        {
            var data = JsonUtility.FromJson<TData>(json);
            repositoryService.Spawn<TResult, TData>(data);
        }
    }
}
