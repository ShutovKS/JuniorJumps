#region

using Data.AssetsAddressables;
using Services.Factories.AbstractFactory;
using Services.SaveLoadProgress;
using UI.MainMenu;
using UnityEngine;
using Zenject;

#endregion

namespace Unit.MainMenu
{
    public class MainMenuController
    {
        [Inject]
        public MainMenuController(
            IAbstractFactory abstractFactory,
            ISaveLoadProgress saveLoadProgress)
        {
            _abstractFactory = abstractFactory;
            _pointsMax = saveLoadProgress.LoadProgress().maxPoints.value;

            CreatedCamera();
            CreatedMainMenuScreen();
        }

        private readonly IAbstractFactory _abstractFactory;

        private readonly int _pointsMax;

        private async void CreatedCamera()
        {
            var camera = await _abstractFactory.CreateInstance<GameObject>(
                AssetsAddressablesContainers.CAMERA);
        }

        private async void CreatedMainMenuScreen()
        {
            var mainMenuScreen = await _abstractFactory.CreateInstance<GameObject>(
                AssetsAddressablesContainers.MAIN_MENU_SCREEN);

            var menu = mainMenuScreen.GetComponent<Menu>();

            menu.SetMaxPoints(_pointsMax);
        }
    }
}