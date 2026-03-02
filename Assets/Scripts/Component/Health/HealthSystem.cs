using UnityEngine;
using Cysharp.Threading.Tasks;

public struct HealthInfo
{
    public Entity target;
    public int amount; //HP modification amount neg or pos
}

public interface IHealthSystem : IDependency<IHealthSystem>
{
    UniTask Apply(HealthInfo info);
}
public class HealthSystem : IHealthSystem
{
    public async UniTask Apply(HealthInfo info)
    {
        if (info.amount > 0)
            await Restore(info);
        else if (info.amount < 0)
            await Damage(info);
        await Present(info);
    }

    async UniTask Restore(HealthInfo info)
    {
        var before = info.target.HitPoints;
        var after = Mathf.Min(before + info.amount, info.target.MaxHitPoints);
        info.target.HitPoints = after;
        if (before == 0)
        {
            await IDyingSystem.Resolve().Revive(info.target);
        }
        await UniTask.CompletedTask;
    }

    async UniTask Damage(HealthInfo info)
    {
        var before = info.target.HitPoints;
        var after = Mathf.Max(before + info.amount, 0);
        info.target.HitPoints = after;
        if (before > 0 && after == 0)
        {
            await IDyingSystem.Resolve().Die(info.target);
        }
        await UniTask.CompletedTask;
    }

    async UniTask Present(HealthInfo info)
    {
        var presentInfo = new HealthPresentationInfo
        {
            target = info.target,
            amount = info.amount
        };
        await IHealthPresenter.Resolve().Present(presentInfo);
    }
}

public partial struct Entity
{
    public float Health
    {
        get {
            var hp = IHitPointSystem.Resolve().Get(this);
            var mhp = IMaxHitPointSystem.Resolve().Get(this);
            return (float)hp / (float)MaxHitPoints;
        }
    }
}