using UnityEngine;
using Cysharp.Threading.Tasks;

public interface IEncounterAssetSystem : IDependency<IEncounterAssetSystem>
{
    UniTask<IEncounter> Load();
    UniTask<IEncounter> Load(string entryName);
}

[Dependency(typeof(IEncounterAssetSystem))]
public class EncounterAssetSystem : IEncounterAssetSystem
{
    public async UniTask<IEncounter> Load()
    {
        var encounterName = IEncounterSystem.Resolve().GetName();
        return await Load(encounterName);
    }

    public async UniTask<IEncounter> Load(string encounterName)
    {
        var assetManager = IAssetManager<GameObject>.Resolve();
        var key = string.Format("Assets/Objects/Encounters/{0}.prefab", encounterName);
        var prefab = await assetManager.LoadAssetAsync(key);
        return prefab.GetComponent<Encounter>();
    }
}