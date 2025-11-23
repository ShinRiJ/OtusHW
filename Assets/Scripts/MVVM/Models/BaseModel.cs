using UnityEngine;

namespace PopUp
{
    abstract public class BaseModel : ScriptableObject
    {
        abstract public void Init();
        abstract public void SaveProgress();
        abstract public void CheckForUpdate();

    }
}