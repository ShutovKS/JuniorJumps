#region

using System;
using System.Threading.Tasks;
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

        private readonly IAbstractFactory _abstractFactory;

        private readonly float _maxPlatformSpawnPositionByX;
        private readonly float _maxPlatformSpawnPositionByY;
        private readonly float _minPlatformSpawnPositionByX;
        private readonly float _minPlatformSpawnPositionByY;

        private readonly string[] _platforms =
        {
            AssetsAddressablesContainers.PLATFORM_DEFAULT,
            AssetsAddressablesContainers.PLATFORM_DESTROYING
        };

        public async void SpawnStartingPlatform(
            Vector2 startSpawnPosition,
            Func<bool> onIsNotJumping,
            UnityAction onActionJump)
        {
            var platform = await SpawnPlatform(startSpawnPosition);
            SetUpPlatform(platform, onIsNotJumping, onActionJump);
        }

        private async void CreatedPlatform(
            float positionByY,
            Func<bool> onIsNotJumping,
            UnityAction onActionsIsJumping)
        {
            var position = new Vector3(
                Random.Range(_minPlatformSpawnPositionByX, _maxPlatformSpawnPositionByX),
                Random.Range(_minPlatformSpawnPositionByY, _maxPlatformSpawnPositionByY) + positionByY);

            var platform = await SpawnPlatform(position);
            SetUpPlatform(platform, onIsNotJumping, onActionsIsJumping);
        }

        private void SetUpPlatform(GameObject platform, Func<bool> onIsNotJumping, UnityAction onActionsIsJumping)
        {
            if (!platform.TryGetComponent<IPlatform>(out var iPlatform)) return;

            var jumping = onActionsIsJumping;
            onActionsIsJumping += () =>
            {
                if (iPlatform.IsCreatedNewPlatform) return;
                CreatedPlatform(iPlatform.PositionByY, onIsNotJumping, jumping);
                iPlatform.IsCreatedNewPlatform = true;
            };

            iPlatform.SetUp(onIsNotJumping, onActionsIsJumping);
        }

        private async Task<GameObject> SpawnPlatform(Vector3 position)
        {
            var random = Random.Range(0, _platforms.Length);
            var platform = _platforms[random];
            var platformInstance = await _abstractFactory.CreateInstance<GameObject>(platform);
            platformInstance.transform.position = position;
            return platformInstance;
        }
    }
}