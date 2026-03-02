using Cysharp.Threading.Tasks;

public interface IMonsterActionFlow : IDependency<IMonsterActionFlow>
{
    UniTask<CombatResult?> Play();
}

public class MonsterActionFlow : IMonsterActionFlow
{
    public async UniTask<CombatResult?> Play()
    {
        await UniTask.CompletedTask;
        // TODO: Handle A.I. for monster to take a turn
        UnityEngine.Debug.Log("Monster turn skipped...");
        return ICombatResultSystem.Resolve().CheckResult();
    }
}