#region

using System;
using UnityEngine.Events;

#endregion

namespace Unit.Platforms
{
    public interface IPlatform
    {
        float PositionByY { get; }
        bool IsCreatedNewPlatform { get; set; }
        Func<bool> IsNotJumping { get; }
        UnityAction ActionsIsJumping { get; }
        void SetUp(Func<bool> isNotJumping, UnityAction actionsIsJumping);
        void PlatformTrigger();
    }
}