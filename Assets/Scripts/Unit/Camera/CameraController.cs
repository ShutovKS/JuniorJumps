#region

using System.Threading.Tasks;
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
        public CameraController(
            IAbstractFactory abstractFactory,
            Transform centerTargetTransform)
        {
            _centerTargetTransform = centerTargetTransform;
            _abstractFactory = abstractFactory;
            CreatedCamera();
        }

        private readonly IAbstractFactory _abstractFactory;
        private readonly Transform _centerTargetTransform;
        private CinemachineVirtualCamera _virtualCamera;

        private async void CreatedCamera()
        {
            var cameraInstance = await _abstractFactory
                .CreateInstance<GameObject>(AssetsAddressablesContainers.CAMERA);

            var virtualCameraInstance = await _abstractFactory
                .CreateInstance<GameObject>(AssetsAddressablesContainers.VIRTUAL_CAMERA);

            _virtualCamera = virtualCameraInstance.GetComponent<CinemachineVirtualCamera>();
            
            _virtualCamera.Follow = _centerTargetTransform;
            _virtualCamera.LookAt = _centerTargetTransform;
        }
    }
}