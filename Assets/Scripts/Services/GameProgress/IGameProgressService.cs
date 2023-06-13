#region

using Data.Dynamic;

#endregion

namespace Services.GameProgress
{
    public interface IGameProgressService
    {
        Progress Progress { get; }

        void SetProgress(Progress progress);
    }
}