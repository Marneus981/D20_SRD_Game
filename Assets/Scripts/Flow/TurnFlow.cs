using Cysharp.Threading.Tasks;

public interface ITurnFlow : IDependency<ITurnFlow>
{
    UniTask<CombatResult?> Play(Entity entity);
}
[Dependency(typeof(ITurnFlow))]
public struct TurnFlow : ITurnFlow
{
    public async UniTask<CombatResult?> Play(Entity entity)
    {
        var system = ITurnSystem.Resolve();
        system.Begin(entity);

        CombatResult? result = null;
        while (!system.IsComplete)
        {
            system.InReach = IReachSystem.Resolve().EntitiesInReach(entity);
            if (entity.Party == Party.Hero)
                result = await IHeroActionFlow.Resolve().Play();
            else
                result = await IMonsterActionFlow.Resolve().Play();

            if (result.HasValue)
                break;
        }
        return result;
    }
}