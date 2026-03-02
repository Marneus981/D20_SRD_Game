using Cysharp.Threading.Tasks;

public interface ICombatFlow : IDependency<ICombatFlow>
{
    UniTask<CombatResult> Play();
}

public struct CombatFlow : ICombatFlow
{
    public async UniTask<CombatResult> Play()
    {
        await Enter();
        CombatResult result = await Loop();
        await Exit();
        return result;
    }

    async UniTask Enter()
    {
        // TODO: initiative, surprise attacks, etc
        await UniTask.CompletedTask;
    }

    async UniTask<CombatResult> Loop()
    {
        CombatResult? result = null;
        while (!result.HasValue)
            result = await IRoundFlow.Resolve().Play();
        return result.Value;
    }

    async UniTask Exit()
    {
        // TODO: award experience, delete monster data, etc
        await UniTask.CompletedTask;
    }
}