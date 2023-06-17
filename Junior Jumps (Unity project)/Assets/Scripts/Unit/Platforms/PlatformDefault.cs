#region

using System;
using UnityEngine.Events;

#endregion

namespace Unit.Platforms
{
    public class PlatformDefault : AbstractPlatform
    {
        public override void SetUp(Func<bool> isNotJumping, UnityAction actionsIsJumping)
        {
            IsNotJumping = isNotJumping;
            ActionsIsJumping = actionsIsJumping;
        }
    }
}