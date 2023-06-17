#region

using System;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Unit.Platforms
{
    public abstract class AbstractPlatform : MonoBehaviour, IPlatform
    {
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            PlatformTrigger();

            if (IsNotJumping != null && IsNotJumping())
            {
                ActionsIsJumping();
            }
        }

        public float PositionByY => transform.position.y;
        public bool IsCreatedNewPlatform { get; set; }
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
    }
}