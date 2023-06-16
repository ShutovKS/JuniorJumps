#region

using Data.Dynamic;

#endregion

namespace Services.SaveLoadProgress
{
    public interface ISaveLoadProgress
    {
        void UpdateProgress(Progress progress);
        Progress LoadProgress();
        void ClearProgress();
        bool HasProgress();
    }
}