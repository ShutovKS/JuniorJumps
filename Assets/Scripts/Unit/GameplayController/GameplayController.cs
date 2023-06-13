#region

using System.Threading.Tasks;
using Data.AssetsAddressables;
using Data.Setting;
using Services.Factories.AbstractFactory;
using Services.Input;
using Unit.Camera;
using Unit.GameObjectNotVisible;
using Unit.Platforms.PlatformsGeneration;
using Unit.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

#endregion

namespace Unit.GameplayController
{
    public class GameplayController
    {
        [Inject]
        public GameplayController(
            IAbstractFactory abstractFactory,
            GameplaySetting gameplaySetting,
            InputActionsReader inputActionsReader)
        {
            _playerTargetTransform = new GameObject("PlayerTarget").transform;
            _centerTargetTransform = new GameObject("CenterTarget").transform;

            _gameplaySetting = gameplaySetting;

            _playerController = new PlayerController(
                abstractFactory,
                gameplaySetting.SpawnPositionPlayer,
                gameplaySetting.PlayerJumpForce,
                gameplaySetting.PlayerMoveSpeed,
                inputActionsReader,
                _playerTargetTransform);

            _cameraController = new CameraController(
                abstractFactory,
                _centerTargetTransform);

            _platformsGeneration = new PlatformsGeneration(
                abstractFactory,
                gameplaySetting.MinPlatformSpawnPositionByX,
                gameplaySetting.MaxPlatformSpawnPositionByX,
                gameplaySetting.MinPlatformSpawnPositionByY,
                gameplaySetting.MaxPlatformSpawnPositionByY,
                _playerController.IsNotJumping,
                _playerController.Jump);

            _playerDead = new PlayerDead(
                _playerTargetTransform,
                _centerTargetTransform,
                gameplaySetting.PlayerDeadDistance,
                MoveToMainMenu);

            Initialize();
        }


        private readonly PlatformsGeneration _platformsGeneration;
        private readonly PlayerController _playerController;
        private readonly CameraController _cameraController;
        private readonly GameplaySetting _gameplaySetting;
        private readonly Transform _playerTargetTransform;
        private readonly Transform _centerTargetTransform;
        private readonly PlayerDead _playerDead;

        private void Initialize()
        {
            _platformsGeneration.Generation(_gameplaySetting.StartPlatformSpawnPositionByY);
            FollowTarget();
        }

        private async void FollowTarget()
        {
            while (true)
            {
                if (_playerTargetTransform.position.y > _centerTargetTransform.position.y)
                {
                    _centerTargetTransform.position = new Vector3(0, _playerTargetTransform.position.y, 0);
                }

                await Task.Yield();
            }
        }

        private void MoveToMainMenu()
        {
            SceneManager.LoadScene(AssetsAddressablesContainers.MAIN_MENU_SCENE);
        }
    }
}