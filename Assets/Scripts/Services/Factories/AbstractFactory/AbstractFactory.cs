#region

using System.Threading.Tasks;
using Services.AssetsAddressables;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

#endregion

namespace Services.Factories.AbstractFactory
{
    public class AbstractFactory : IAbstractFactory
    {
        [Inject]
        public AbstractFactory(IAssetsAddressablesProvider assetsAddressablesProvider)
        {
            _assetsAddressablesProvider = assetsAddressablesProvider;
        }

        private readonly IAssetsAddressablesProvider _assetsAddressablesProvider;

        public async Task<T> CreateInstance<T>(string path) where T : Object
        {
            var prefab = await _assetsAddressablesProvider.GetAsset<T>(path);

            var instance = Object.Instantiate(prefab);

            return instance;
        }

        public async Task<T> CreateInstance<T>(AssetReference path) where T : Object
        {
            var prefab = await _assetsAddressablesProvider.GetAsset<T>(path);

            var instance = Object.Instantiate(prefab);

            return instance;
        }
    }
}