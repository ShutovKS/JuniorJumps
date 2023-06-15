using System;
using UnityEngine;
using UnityEngine.Events;

namespace Unit.Platforms
{
    public abstract class AbstractPlatform : MonoBehaviour, IPlatform
    {
        public Func<bool> IsNotJumping { get; protected set; }
        public UnityAction ActionsIsJumping { get; protected set; }
        public abstract void SetUp(Func<bool> isNotJumping, UnityAction actionsIsJumping);

        public void PlatformTrigger()
        {
            if (IsNotJumping != null && IsNotJumping())
            {
                ActionsIsJumping();
            }
        }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            PlatformTrigger();

            if (IsNotJumping != null && IsNotJumping())
            {
                ActionsIsJumping();
            }
        }
    }
}