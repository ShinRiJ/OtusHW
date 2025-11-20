using System;

namespace SaveGameHW
{
    [Serializable]
    public class SaveEntity
    {
        public String Type;
        public String Json;

        public SaveEntity(String type, String json)
        {
            Type = type;
            Json = json;
        }
    }
}
