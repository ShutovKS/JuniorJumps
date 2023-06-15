#region

using System;
using System.Threading.Tasks;
using Data.AssetsAddressables;
using Data.Dynamic;
using Data.Setting;
using Services.SaveLoadProgress;
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
            GameplaySetting gameplaySetting,
            PlayerSetting playerSetting,
            PlatformsGeneration platformsGeneration,
            CameraController cameraController,
            PlayerController playerController,
            ISaveLoadProgress saveLoadProgress)
        {
            _playerTargetTransform = new GameObject("PlayerTarget").transform;
            _centerTargetTransform = new GameObject("CenterTarget").transform;

            _spawnPositionPlayer = gameplaySetting.SpawnPositionPlayer;
            _startPlatformSpawnPositionByY = gameplaySetting.StartPlatformSpawnPositionByY;

            _playerDeadDistance = playerSetting.DeadDistance;

            _playerController = playerController;
            _cameraController = cameraController;
            _platformsGeneration = platformsGeneration;
            _saveLoadProgress = saveLoadProgress;

            Initialize();
        }

        private readonly CameraController _cameraController;
        private readonly Transform _centerTargetTransform;
        private readonly PlatformsGeneration _platformsGeneration;
        private readonly PlayerController _playerController;
        private readonly ISaveLoadProgress _saveLoadProgress;

        private readonly Transform _playerTargetTransform;
        private readonly Vector2 _spawnPositionPlayer;
        private readonly float _startPlatformSpawnPositionByY;
        private readonly float _playerDeadDistance;

        private UnityAction _onActionsIsJumping;
        private Func<bool> _onIsNotJumping;

        private Progress _currentProgress;
        private float _currentMaxPoints => _centerTargetTransform.position.y;

        private void Initialize()
        {
            _currentProgress = _saveLoadProgress.LoadProgress();

            _playerController.CreatePlayer(
                _playerTargetTransform,
                _spawnPositionPlayer);

            _onIsNotJumping = _playerController.IsNotJumping;
            _onActionsIsJumping = _playerController.Jump;

            _cameraController.CreatedCamera(
                _centerTargetTransform);

            _platformsGeneration.Generation(
                new Vector2(0, _startPlatformSpawnPositionByY),
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
                else if (_playerTargetTransform.position.y < _centerTargetTransform.position.y - _playerDeadDistance)
                {
                    SaveProgress();
                    MoveToMainMenu();
                    return;
                }

                await Task.Yield();
            }
        }

        private void SaveProgress()
        {
            if (_currentProgress.maxPoints.value < _currentMaxPoints)
            {
                _currentProgress.maxPoints.value = (int)_currentMaxPoints;
                _saveLoadProgress.UpdateProgress(_currentProgress);
            }
        }

        private void MoveToMainMenu()
        {
            SceneManager.LoadScene(AssetsAddressablesContainers.MAIN_MENU_SCENE);
        }
    }
}