namespace SaveGameHW
{
    public interface ISaveable<TData> : ISaveableBase 
    {
        TData SaveData();
        void LoadData(TData data);
    }
}
