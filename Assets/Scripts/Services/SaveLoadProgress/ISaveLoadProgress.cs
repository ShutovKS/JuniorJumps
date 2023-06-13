namespace Services.SaveLoadProgress
{
    public interface ISaveLoadProgress
    {
        void SaveProgress();
        void LoadProgress();
        void ClearProgress();
        bool HasProgress();
    }
}