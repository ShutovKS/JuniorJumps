﻿#region

using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

#endregion

namespace Services.Factories.AbstractFactory
{
    public interface IAbstractFactory
    {
        Task<T> CreateInstance<T>(string path) where T : Object;
        Task<T> CreateInstance<T>(AssetReference path) where T : Object;
    }
}