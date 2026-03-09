using UnityEngine;
using Cysharp.Threading.Tasks;

public interface IAncestryAssetSystem : IDependency<IAncestryAssetSystem>
{
    UniTask<IAncestry> Load(string name);
}

public class AncestryAssetSystem : IAncestryAssetSystem
{
    public async UniTask<IAncestry> Load(string name)
    {
        var assetManager = IAssetManager<GameObject>.Resolve();
        var key = string.Format("Assets/AutoGeneration/Ancestries/{0}.prefab", name);
        var prefab = await assetManager.LoadAssetAsync(key);
        return prefab.GetComponent<IAncestry>();
    }
}