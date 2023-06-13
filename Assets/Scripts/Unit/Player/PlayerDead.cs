#region

using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Unit.GameObjectNotVisible
{
    public class PlayerDead
    {
        public PlayerDead(
            Transform playerTransform, 
            Transform centerTargetTransform,
            float distanceToDeath, 
            UnityAction onDead)
        {
            _playerTransform = playerTransform;
            _centerTargetTransform = centerTargetTransform;
            _distanceToDeath = distanceToDeath;
            _onDead = onDead;

            FollowDead();
        }

        private readonly Transform _centerTargetTransform;
        private readonly Transform _playerTransform;
        private readonly float _distanceToDeath;
        private readonly UnityAction _onDead;

        private async void FollowDead()
        {
            while (true)
            {
                if (_playerTransform.position.y < _centerTargetTransform.position.y - _distanceToDeath)
                {
                    _onDead?.Invoke();
                }

                await Task.Yield();
            }
        }
    }
}