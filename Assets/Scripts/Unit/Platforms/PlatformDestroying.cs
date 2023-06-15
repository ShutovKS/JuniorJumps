#region

using System;
using UnityEngine.Events;

#endregion

namespace Unit.Platforms
{
    public class PlatformDestroying : AbstractPlatform
    {
        public override void SetUp(Func<bool> isNotJumping, UnityAction actionsIsJumping)
        {
            IsNotJumping = isNotJumping;
            ActionsIsJumping += actionsIsJumping;
            ActionsIsJumping += () => Destroy(gameObject, 0.5f);
        }
    }
}