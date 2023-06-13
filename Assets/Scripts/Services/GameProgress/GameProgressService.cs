#region

using Data.Dynamic;

#endregion

namespace Services.GameProgress
{
    public class GameProgressService : IGameProgressService
    {
        public Progress Progress { get; private set; }

        public void SetProgress(Progress progress)
        {
            Progress = progress;
        }
    }
}