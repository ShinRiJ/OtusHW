using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveGameHW
{
    [CreateAssetMenu(fileName = "Bar", menuName = "Installers/Bar")]
    public class SavedPrefabByTypeData : ScriptableObject
    {
        public enum PrefabType
        {
            Player,
            Enemy,
            NPC
        }
    }
}
