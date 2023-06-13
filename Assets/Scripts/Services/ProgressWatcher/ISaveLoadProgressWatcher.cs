#region

using Data.Dynamic;

#endregion

namespace Services.ProgressWatcher
{
    public interface ISaveLoadProgressWatcher
    {
        void LoadProgress(Progress progress);
        void UpdateProgress();
    }
}