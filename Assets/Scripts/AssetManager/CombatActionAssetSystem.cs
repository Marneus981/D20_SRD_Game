using UnityEngine;
using Cysharp.Threading.Tasks;

public interface ICombatActionAssetSystem : IDependency<ICombatActionAssetSystem>
{
    UniTask<ICombatAction> Load(string assetName);
}

public class CombatActionAssetSystem : ICombatActionAssetSystem
{
    public async UniTask<ICombatAction> Load(string assetName)
    {
        var assetManager = IAssetManager<GameObject>.Resolve();
        var key = string.Format("Assets/Objects/CombatAction/{0}.prefab", assetName);
        var prefab = await assetManager.LoadAssetAsync(key);
        return prefab.GetComponent<ICombatAction>();
    }
}