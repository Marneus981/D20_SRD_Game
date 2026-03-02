using UnityEngine;
using Cysharp.Threading.Tasks;

public struct DyingPresentationInfo
{
    public Entity entity;
    public bool value;
}

public interface IDyingPresenter : IDependency<IDyingPresenter>
{
    UniTask Present(DyingPresentationInfo info);
}

public class DyingPresenter : MonoBehaviour, IDyingPresenter
{
    public async UniTask Present(DyingPresentationInfo info)
    {
        var view = IEntityViewProvider.Resolve().GetView(info.entity, ViewZone.Combatant);
        var combatant = view.GetComponent<CombatantView>();
        var animation = info.value ? CombatantAnimation.Death : CombatantAnimation.Idle;
        ICombatantViewSystem.Resolve().SetAnimation(combatant, animation);
        await UniTask.CompletedTask;
    }

    private void OnEnable()
    {
        IDyingPresenter.Register(this);
    }

    private void OnDisable()
    {
        IDyingPresenter.Reset();
    }
}