#region

using Unit.GameplayController;
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

            BindGameplayController();
        }

        private void BindGameplayController()
        {
            Container.Bind<GameplayController>().AsSingle().NonLazy();
        }
    }
}