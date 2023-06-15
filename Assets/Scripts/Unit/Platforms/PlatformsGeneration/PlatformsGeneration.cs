#region

using System;
using Data.AssetsAddressables;
using Data.Setting;
using Services.Factories.AbstractFactory;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

#endregion

namespace Unit.Platforms.PlatformsGeneration
{
    public class PlatformsGeneration
    {
        [Inject]
        public PlatformsGeneration(
            IAbstractFactory abstractFactory,
            GameplaySetting gameplaySetting)
        {
            _abstractFactory = abstractFactory;

            _minPlatformSpawnPositionByX = gameplaySetting.MinPlatformSpawnPositionByX;
            _maxPlatformSpawnPositionByX = gameplaySetting.MaxPlatformSpawnPositionByX;
            _minPlatformSpawnPositionByY = gameplaySetting.MinPlatformSpawnPositionByY;
            _maxPlatformSpawnPositionByY = gameplaySetting.MaxPlatformSpawnPositionByY;
        }

        private const string PLATFORM_DEFAULT = AssetsAddressablesContainers.PLATFORM_DEFAULT;
        private const string PLATFORM_DESTROYING = AssetsAddressablesContainers.PLATFORM_DESTROYING;

        private readonly IAbstractFactory _abstractFactory;

        private readonly float _maxPlatformSpawnPositionByX;
        private readonly float _maxPlatformSpawnPositionByY;
        private readonly float _minPlatformSpawnPositionByX;
        private readonly float _minPlatformSpawnPositionByY;

        public async void Generation(
            Vector2 startSpawnPosition,
            Func<bool> onIsNotJumping,
            UnityAction onActionJump)
        {
            var spawnPosition = startSpawnPosition;

            for (var i = 0; i < 1000; i++)
            {
                CreatedPlatform(
                    spawnPosition,
                    i % 2 == 0 ? PLATFORM_DEFAULT : PLATFORM_DESTROYING,
                    onIsNotJumping,
                    onActionJump);

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
            Func<bool> onIsNotJumping,
            UnityAction onActionsIsJumping)
        {
            var platform = await _abstractFactory.CreateInstance<GameObject>(typePlatform);

            platform.transform.position = position;

            if (platform.TryGetComponent<IPlatform>(out var iPlatform))
                iPlatform.SetUp(onIsNotJumping, onActionsIsJumping);
        }
    }
}