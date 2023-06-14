#region

using System;
using System.Threading.Tasks;
using Data.AssetsAddressables;
using Data.Setting;
using Services.Factories.AbstractFactory;
using Services.Input;
using Unit.Camera;
using Unit.Platforms.PlatformsGeneration;
using Unit.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Zenject;

#endregion

namespace Unit.Gameplay
{
    public class GameplayController
    {
        [Inject]
        public GameplayController(
            IAbstractFactory abstractFactory,
            GameplaySetting gameplaySetting,
            InputActionsReader inputActionsReader,
            PlatformsGeneration platformsGeneration,
            CameraController cameraController,
            PlayerController playerController)
        {
            _playerTargetTransform = new GameObject("PlayerTarget").transform;
            _centerTargetTransform = new GameObject("CenterTarget").transform;

            _gameplaySetting = gameplaySetting;
            _playerController = playerController;
            _cameraController = cameraController;
            _platformsGeneration = platformsGeneration;

            Initialize();
        }

        private readonly CameraController _cameraController;
        private readonly Transform _centerTargetTransform;
        private readonly GameplaySetting _gameplaySetting;

        private readonly PlatformsGeneration _platformsGeneration;
        private readonly PlayerController _playerController;

        private readonly Transform _playerTargetTransform;
        private UnityAction _onActionsIsJumping;

        private Func<bool> _onIsNotJumping;

        private void Initialize()
        {
            _playerController.CreatePlayer(
                _playerTargetTransform,
                _gameplaySetting.SpawnPositionPlayer);

            _onIsNotJumping = _playerController.IsNotJumping;
            _onActionsIsJumping = _playerController.Jump;

            _cameraController.CreatedCamera(
                _centerTargetTransform);

            _platformsGeneration.Generation(
                new Vector2(0, _gameplaySetting.StartPlatformSpawnPositionByY),
                _onIsNotJumping,
                _onActionsIsJumping);

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
                else if (_playerTargetTransform.position.y <
                         _centerTargetTransform.position.y - _gameplaySetting.PlayerDeadDistance)
                {
                    MoveToMainMenu();
                    return;
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