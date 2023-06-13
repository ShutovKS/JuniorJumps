#region

using Data.Dynamic;
using Services.GameProgress;
using Services.ProgressWatcher;
using UnityEngine;
using Zenject;

#endregion

namespace Services.SaveLoadProgress
{
    public class SaveLoadProgress : ISaveLoadProgress
    {
        private const string PROGRESS_KEY = "Progress";

        private IGameProgressService _gameProgressService;
        private IProgressWatcher _progressWatcher;

        public void SaveProgress()
        {
            foreach (var watcher in _progressWatcher.ProgressWatchers)
            {
                watcher.UpdateProgress();
            }

            PlayerPrefs.SetString(PROGRESS_KEY, JsonUtility.ToJson(_gameProgressService.Progress));
        }

        public void LoadProgress()
        {
            if (!HasProgress()) return;

            var progress = JsonUtility.FromJson<Progress>(PlayerPrefs.GetString(PROGRESS_KEY));
            _gameProgressService.SetProgress(progress);

            foreach (var watcher in _progressWatcher.ProgressWatchers)
            {
                watcher.LoadProgress(progress);
            }
        }

        public void ClearProgress()
        {
            PlayerPrefs.DeleteKey(PROGRESS_KEY);
        }

        public bool HasProgress()
        {
            return PlayerPrefs.HasKey(PROGRESS_KEY);
        }

        [Inject]
        private void Construct(IGameProgressService gameProgressService, IProgressWatcher progressWatcher)
        {
            _gameProgressService = gameProgressService;
            _progressWatcher = progressWatcher;
        }
    }
}