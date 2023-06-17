#region

using Unit.Camera;
using Unit.Gameplay;
using Unit.Platforms.PlatformsGeneration;
using Unit.Player;
using UnityEngine;
using Zenject;

#endregion

namespace Infrastructure.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("GameplayInstaller InstallBindings");

            BindCameraController();
            BindPlatformsGeneration();
            BindPlayerController();

            BindGameplayController();
        }

        private void BindCameraController()
        {
            Container.Bind<CameraController>().AsSingle();
        }

        private void BindPlatformsGeneration()
        {
            Container.Bind<PlatformsGeneration>().AsSingle();
        }

        private void BindPlayerController()
        {
            Container.Bind<PlayerController>().AsSingle();
        }

        private void BindGameplayController()
        {
            Container.Bind<GameplayController>().AsSingle().NonLazy();
        }
    }
}