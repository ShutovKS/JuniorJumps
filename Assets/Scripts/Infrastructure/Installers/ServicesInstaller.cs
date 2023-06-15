#region

using Data.Setting;
using Services.AssetsAddressables;
using Services.Factories.AbstractFactory;
using Services.Input;
using Services.SaveLoadProgress;
using UnityEngine;
using Zenject;

#endregion

namespace Infrastructure.Installers
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField] private InputActionsReader _inputActionsReader;
        [SerializeField] private GameplaySetting _gameplaySetting;
        [SerializeField] private PlayerSetting _playerSetting;

        public override void InstallBindings()
        {
            Debug.Log("ServicesInstaller InstallBindings");

            BindAssetsAdressables();
            BindInputActions();
            BindSetting();
            BindSaveLoadProgress();
            BindFactory();
        }

        private void BindAssetsAdressables()
        {
            Container.Bind<IAssetsAddressablesProvider>().To<AssetsAddressablesProvider>().AsSingle();
        }

        private void BindSaveLoadProgress()
        {
            Container.Bind<ISaveLoadProgress>().To<SaveLoadProgress>().AsSingle();
        }

        private void BindFactory()
        {
            Container.Bind<IAbstractFactory>().To<AbstractFactory>().AsSingle();
        }

        private void BindInputActions()
        {
            Container.Bind<InputActionsReader>().FromInstance(_inputActionsReader).AsSingle();
        }

        private void BindSetting()
        {
            Container.Bind<GameplaySetting>().FromInstance(_gameplaySetting).AsSingle();
            Container.Bind<PlayerSetting>().FromInstance(_playerSetting).AsSingle();
        }
    }
}