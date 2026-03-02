using Cysharp.Threading.Tasks;

public interface IMonsterActionFlow : IDependency<IMonsterActionFlow>
{
    UniTask<CombatResult?> Play();
}

public class MonsterActionFlow : IMonsterActionFlow
{
    public async UniTask<CombatResult?> Play()
    {
        var current = ITurnSystem.Resolve().Current;
        var actionName = current.EncounterActions.names[0];
        var action = await ICombatActionAssetSystem.Resolve().Load(actionName);
        await action.Perform(current);
        return ICombatResultSystem.Resolve().CheckResult();
    }
}