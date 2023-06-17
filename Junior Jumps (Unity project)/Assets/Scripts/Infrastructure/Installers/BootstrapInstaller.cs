#region

using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

#endregion

namespace Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("BootstrapInstaller InstallBindings");

            SceneManager.LoadScene("MainMenu");
        }
    }
}