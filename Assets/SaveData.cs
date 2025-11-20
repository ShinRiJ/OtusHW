using System;
using System.Collections.Generic;

namespace SaveGameHW
{
    [Serializable]
    public class SaveData
    {
        public List<SaveEntity> Entities = new List<SaveEntity>();
    }
}
