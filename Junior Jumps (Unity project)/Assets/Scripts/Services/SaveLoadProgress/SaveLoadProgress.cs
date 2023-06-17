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
        }

        public Progress LoadProgress()
        {
            var progressJson = PlayerPrefs.GetString(PROGRESS_KEY);
            var progress = JsonUtility.FromJson<Progress>(progressJson) ?? new Progress();

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