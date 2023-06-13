#region

using System;
using Data.AssetsAddressables;
using Services.Factories.AbstractFactory;
using Unit.Player;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

#endregion

namespace Unit.Platforms.PlatformsGeneration
{
    public class PlatformsGeneration
    {
        public PlatformsGeneration(
            IAbstractFactory abstractFactory,
            float minPlatformSpawnPositionByX,
            float maxPlatformSpawnPositionByX,
            float minPlatformSpawnPositionByY,
            float maxPlatformSpawnPositionByY,
            Func<bool> isNotJumping,
            UnityAction actionJump)
        {
            _abstractFactory = abstractFactory;

            _minPlatformSpawnPositionByX = minPlatformSpawnPositionByX;
            _maxPlatformSpawnPositionByX = maxPlatformSpawnPositionByX;
            _minPlatformSpawnPositionByY = minPlatformSpawnPositionByY;
            _maxPlatformSpawnPositionByY = maxPlatformSpawnPositionByY;

            _isNotJumping = isNotJumping;
            _actionJump = actionJump;
        }

        private const string PLATFORM_DEFAULT = AssetsAddressablesContainers.PLATFORM_DEFAULT;

        private readonly IAbstractFactory _abstractFactory;
        
        private readonly float _maxPlatformSpawnPositionByX;
        private readonly float _maxPlatformSpawnPositionByY;
        private readonly float _minPlatformSpawnPositionByX;
        private readonly float _minPlatformSpawnPositionByY;
        
        private readonly Func<bool> _isNotJumping;
        private readonly UnityAction _actionJump;

        public async void Generation(float startSpawnPositionByY)
        {
            var spawnPosition = new Vector2(0, startSpawnPositionByY);


            for (var i = 0; i < 1000; i++)
            {
                CreatedPlatform(spawnPosition, PLATFORM_DEFAULT, _isNotJumping, _actionJump);

                spawnPosition.y += Random.Range(
                    _minPlatformSpawnPositionByY,
                    _maxPlatformSpawnPositionByY);

                spawnPosition.x = Random.Range(
                    _minPlatformSpawnPositionByX,
                    _maxPlatformSpawnPositionByX);
            }
        }

        private async void CreatedPlatform(
            Vector2 position,
            string typePlatform,
            Func<bool> isNotJumping,
            UnityAction actionsIsJumping)
        {
            var platform = await _abstractFactory.CreateInstance<GameObject>(typePlatform);

            platform.transform.position = position;

            if (platform.TryGetComponent<IPlatform>(out var iPlatform))
                iPlatform.SetUp(isNotJumping, actionsIsJumping);
        }
    }
}