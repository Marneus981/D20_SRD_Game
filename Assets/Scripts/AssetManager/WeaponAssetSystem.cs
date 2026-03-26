using UnityEngine;
using Cysharp.Threading.Tasks;

public interface IWeaponAssetSystem : IDependency<IWeaponAssetSystem>
{
    UniTask<GameObject> Load(string name);
    UniTask<Entity> Spawn(string name);
}

[Dependency(typeof(IWeaponAssetSystem))]
public class WeaponAssetSystem : IWeaponAssetSystem
{
    public async UniTask<GameObject> Load(string name)
    {
        var assetManager = IAssetManager<GameObject>.Resolve();
        var key = string.Format("Assets/AutoGeneration/Weapons/{0}.prefab", name);
        var prefab = await assetManager.LoadAssetAsync(key);
        return prefab;
    }

    public async UniTask<Entity> Spawn(string name)
    {
        var prefab = await Load(name);
        var providers = prefab.GetComponents<IAttributeProvider>();
        var weapon = IEntitySystem.Resolve().Create();
        foreach (var provider in providers)
            await provider.SetupFlow(weapon);
        return weapon;
    }
}