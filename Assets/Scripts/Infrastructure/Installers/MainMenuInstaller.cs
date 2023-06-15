using Unit.MainMenu;
using Zenject;

namespace Infrastructure.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMainMenuController();
        }
        
        private void BindMainMenuController()
        {
            Container.Bind<MainMenuController>().AsSingle().NonLazy();
        }
    }
}