using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public interface IEncounterFlow : IDependency<IEncounterFlow>
{
    UniTask Play();
}

public class EncounterFlow : IEncounterFlow
{
    public async UniTask Play()
    {
        var encounter = await Enter();
        var combatResult = await Loop();
        await Exit(encounter, combatResult);
    }

    async UniTask<IEncounter> Enter()
    {
        await SceneManager.LoadSceneAsync("Encounter").ToUniTask();
        var asset = await IEncounterAssetSystem.Resolve().Load();
        await IEncounterSystem.Resolve().Setup(asset);
        return asset;
    }

    async UniTask<CombatResult> Loop()
    {
        CombatResult? combatResult = null;
        while (!combatResult.HasValue)
        {
            await UniTask.NextFrame();
            combatResult = await ICombatFlow.Resolve().Play();
        }
        return combatResult.Value;
    }

    async UniTask Exit(IEncounter asset, CombatResult result)
    {
        switch (result)
        {
            case CombatResult.Victory:
                IEntrySystem.Resolve().SetName(asset.VictoryEntry);
                break;
            case CombatResult.Defeat:
                IEntrySystem.Resolve().SetName(asset.DefeatEntry);
                break;
        }
        DeleteMonsters();
        await UniTask.CompletedTask;
    }
    void DeleteMonsters()
    {
        var system = IEntitySystem.Resolve();
        var table = new List<Entity>(ICombatantSystem.Resolve().Table);//Copy bc we cant mod a collection while enumerating it
        foreach (var entity in table)
            if (entity.Party == Party.Monster)
                system.Destroy(entity);
    }
}