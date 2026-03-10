using UnityEngine;
using Cysharp.Threading.Tasks;

public interface IBackgroundAssetSystem : IDependency<IBackgroundAssetSystem>
{
    UniTask<IBackground> Load(string name);
}

public class BackgroundAssetSystem : IBackgroundAssetSystem
{
    public async UniTask<IBackground> Load(string name)
    {
        var assetManager = IAssetManager<GameObject>.Resolve();
        var key = string.Format("Assets/AutoGeneration/Backgrounds/{0}.prefab", name);
        var prefab = await assetManager.LoadAssetAsync(key);
        return prefab.GetComponent<IBackground>();
    }
}