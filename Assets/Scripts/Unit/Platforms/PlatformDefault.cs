#region

using System;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Unit.Platforms
{
    public class PlatformDefault : MonoBehaviour, IPlatform
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PlatformTrigger();
            }
        }

        public Func<bool> IsNotJumping { get; private set; }
        public UnityAction ActionsIsJumping { get; private set; }

        public void SetUp(Func<bool> isNotJumping, UnityAction actionsIsJumping)
        {
            IsNotJumping = isNotJumping;
            ActionsIsJumping = actionsIsJumping;
        }

        public void PlatformTrigger()
        {
            if (IsNotJumping != null && IsNotJumping())
            {
                ActionsIsJumping();
            }
        }
    }
}