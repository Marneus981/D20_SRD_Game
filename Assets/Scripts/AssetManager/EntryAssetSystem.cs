using UnityEngine;
using Cysharp.Threading.Tasks;

public interface IEntryAssetSystem : IDependency<IEntryAssetSystem>
{
    UniTask<IEntry> Load();
    UniTask<IEntry> Load(string entryName);
}

public class EntryAssetSystem : IEntryAssetSystem
{
    //Two different “Load” methods: 
    public async UniTask<IEntry> Load()
    //If you don’t specify the name of the asset to load, 
    //it will assume you wanted to load the “current” Entry – 
    //the one that is obtained via the EntrySystem. 
    {
        var entryName = IEntrySystem.Resolve().GetName();
        return await Load(entryName);
    }

    public async UniTask<IEntry> Load(string entryName)
    //Specify the asset name, in case you don’t want the default. 
    //This could be used for testing etc.
    {
        var assetManager = IAssetManager<GameObject>.Resolve();
        var key = string.Format("Assets/Objects/Entries/{0}.prefab", entryName);
        var asset = await assetManager.LoadAssetAsync(key);
        return asset.GetComponent<IEntry>();
    }
}