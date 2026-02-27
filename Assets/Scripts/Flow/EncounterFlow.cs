using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

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
        return asset;
    }

    async UniTask<CombatResult> Loop()
    {
        CombatResult? combatResult = null;
        while (!combatResult.HasValue)
        {
            await UniTask.NextFrame();
            //combatResult = ICombatResultSystem.Resolve().CheckResult();
            combatResult = await IHeroActionFlow.Resolve().Play();
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
        await UniTask.CompletedTask;
    }
}