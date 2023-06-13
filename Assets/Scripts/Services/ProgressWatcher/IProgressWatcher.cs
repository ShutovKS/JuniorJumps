#region

using System.Collections.Generic;

#endregion

namespace Services.ProgressWatcher
{
    public interface IProgressWatcher
    {
        List<ISaveLoadProgressWatcher> ProgressWatchers { get; }

        void RegisterProgressWatcher(params ISaveLoadProgressWatcher[] saveLoadProgressWatchers);
        void UnregisterProgressWatcher(params ISaveLoadProgressWatcher[] saveLoadProgressWatchers);
        void ClearProgressWatchers();
    }
}