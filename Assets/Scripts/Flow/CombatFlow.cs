using Cysharp.Threading.Tasks;
using System;

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
        await UniTask.Delay(TimeSpan.FromSeconds(3), ignoreTimeScale: false);//3 sec pause after combat ends, to appreciate it
    }
}