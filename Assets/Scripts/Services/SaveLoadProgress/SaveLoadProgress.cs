#region

using Data.Dynamic;
using UnityEngine;

#endregion

namespace Services.SaveLoadProgress
{
    public class SaveLoadProgress : ISaveLoadProgress
    {
        private const string PROGRESS_KEY = "ProgressKey";

        public void UpdateProgress(Progress progress)
        {
            var progressJson = JsonUtility.ToJson(progress);
            PlayerPrefs.SetString(PROGRESS_KEY, progressJson);

            Debug.Log($"UpdateProgress {progressJson}");
            Debug.Log($"UpdateProgress {progress.maxPoints.value}");
        }

        public Progress LoadProgress()
        {
            var progressJson = PlayerPrefs.GetString(PROGRESS_KEY);
            var progress = JsonUtility.FromJson<Progress>(progressJson) ?? new Progress();
            
            Debug.Log($"LoadProgress {progress.maxPoints.value}");

            return progress;
        }

        public void ClearProgress()
        {
            PlayerPrefs.DeleteKey(PROGRESS_KEY);
        }

        public bool HasProgress()
        {
            return PlayerPrefs.HasKey(PROGRESS_KEY);
        }
    }
}