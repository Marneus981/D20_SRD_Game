using Cysharp.Threading.Tasks;

public interface IMonsterActionFlow : IDependency<IMonsterActionFlow>
{
    UniTask<CombatResult?> Play();
}

public class MonsterActionFlow : IMonsterActionFlow
{
    public async UniTask<CombatResult?> Play()
    //Gambit option: perform first performable action
    {
        var current = ITurnSystem.Resolve().Current;
        ICombatSelectionIndicator.Resolve().Mark(current);//Focus camera even if marker is invisible
        bool didAct = false;
        foreach (var actionName in current.EncounterActions.names)
        {
            var action = await ICombatActionAssetSystem.Resolve().Load(actionName);
            if (action.CanPerform(current) && current.HitPoints > 0)//Placeholder check
            {
                await action.Perform(current);
                didAct = true;
                break;
            }
        }

        if (!didAct)
            ITurnSystem.Resolve().TakeAction(3, false);

        return ICombatResultSystem.Resolve().CheckResult();
    }
}