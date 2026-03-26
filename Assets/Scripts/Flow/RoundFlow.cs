using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

public interface IRoundFlow : IDependency<IRoundFlow>
{
    UniTask<CombatResult?> Play();
}
[Dependency(typeof(IRoundFlow))]
public class RoundFlow : IRoundFlow
{
    public async UniTask<CombatResult?> Play()
    {
        var entities = new List<Entity>(ICombatantSystem.Resolve().Table);
        var turnOrder = entities.OrderByDescending(e => e.Initiative).ToList();

        var system = IRoundSystem.Resolve();
        system.Begin(turnOrder);

        CombatResult? result = null;
        while (!system.IsComplete)
        {
            var entity = system.Next();
            result = await ITurnFlow.Resolve().Play(entity);
            if (result.HasValue)
                break;
        }
        return result;
    }
}