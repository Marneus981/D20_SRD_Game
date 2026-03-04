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
        if (action.CanPerform(current))
            await action.Perform(current);
        else
            ITurnSystem.Resolve().TakeAction(3, false);
        return ICombatResultSystem.Resolve().CheckResult();
    }
}