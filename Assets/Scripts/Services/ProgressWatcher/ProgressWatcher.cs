#region

using System.Collections.Generic;

#endregion

namespace Services.ProgressWatcher
{
    public class ProgressWatcher : IProgressWatcher
    {
        public List<ISaveLoadProgressWatcher> ProgressWatchers { get; } = new();

        public void RegisterProgressWatcher(params ISaveLoadProgressWatcher[] saveLoadProgressWatchers)
        {
            foreach (var watcher in saveLoadProgressWatchers)
            {
                ProgressWatchers.Add(watcher);
            }
        }

        public void UnregisterProgressWatcher(params ISaveLoadProgressWatcher[] saveLoadProgressWatchers)
        {
            foreach (var watcher in saveLoadProgressWatchers)
            {
                ProgressWatchers.Remove(watcher);
            }
        }

        public void ClearProgressWatchers()
        {
            ProgressWatchers.Clear();
        }
    }
}