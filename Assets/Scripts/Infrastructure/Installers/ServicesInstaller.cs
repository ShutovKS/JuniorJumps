#region

using Data.Setting;
using Services.AssetsAddressables;
using Services.Factories.AbstractFactory;
using Services.GameProgress;
using Services.Input;
using Services.ProgressWatcher;
using Services.SaveLoadProgress;
using Services.StaticData;
using UnityEngine;
using Zenject;

#endregion

namespace Infrastructure.Installers
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField] private InputActionsReader _inputActionsReader;
        [SerializeField] private GameplaySetting _gameplaySetting;
        
        public override void InstallBindings()
        {
            Debug.Log("ServicesInstaller InstallBindings");

            BindAssetsAdressables();
            BindInputActions();
            BindStaticData();
            BindSetting();
            BindGameProgress();
            BindProgressWatcher();
            BindSaveLoadProgress();
            BindFactory();
        }

        private void BindAssetsAdressables()
        {
            Container.Bind<IAssetsAddressablesProvider>().To<AssetsAddressablesProvider>().AsSingle();
        }

        private void BindGameProgress()
        {
            Container.Bind<IGameProgressService>().To<GameProgressService>().AsSingle();
        }

        private void BindProgressWatcher()
        {
            Container.Bind<IProgressWatcher>().To<ProgressWatcher>().AsSingle();
        }

        private void BindSaveLoadProgress()
        {
            Container.Bind<ISaveLoadProgress>().To<SaveLoadProgress>().AsSingle();
        }

        private void BindStaticData()
        {
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
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
        }
    }
}