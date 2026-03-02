using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public interface IRoundFlow : IDependency<IRoundFlow>
{
    UniTask<CombatResult?> Play();
}

public class RoundFlow : IRoundFlow
{
    public async UniTask<CombatResult?> Play()
    {
        // TODO: These entities will be provided based on their initiative
        var entities = new List<Entity>(ICombatantSystem.Resolve().Table);

        var system = IRoundSystem.Resolve();
        system.Begin(entities);

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