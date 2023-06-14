#region

using Cinemachine;
using Data.AssetsAddressables;
using Services.Factories.AbstractFactory;
using UnityEngine;
using Zenject;

#endregion

namespace Unit.Camera
{
    public class CameraController
    {
        [Inject]
        public CameraController(
            IAbstractFactory abstractFactory)
        {
            _abstractFactory = abstractFactory;
        }

        private readonly IAbstractFactory _abstractFactory;

        public async void CreatedCamera(Transform targetTransform)
        {
            var cameraInstance = await _abstractFactory
                .CreateInstance<GameObject>(AssetsAddressablesContainers.CAMERA);

            var virtualCameraInstance = await _abstractFactory
                .CreateInstance<GameObject>(AssetsAddressablesContainers.VIRTUAL_CAMERA);

            var virtualCamera = virtualCameraInstance.GetComponent<CinemachineVirtualCamera>();

            virtualCamera.Follow = targetTransform;
            virtualCamera.LookAt = targetTransform;
        }
    }
}