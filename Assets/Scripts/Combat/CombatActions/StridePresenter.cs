using UnityEngine;
using Cysharp.Threading.Tasks;

public struct StridePresentationInfo
{
    public Entity entity;
    public Point fromPosition;
    public Point toPosition;
}

public interface IStridePresenter : IDependency<IStridePresenter>
{
    UniTask Present(StridePresentationInfo info);
}

public class StridePresenter : MonoBehaviour, IStridePresenter
{
    [SerializeField] float speedMultiplier = 0.25f;

    public async UniTask Present(StridePresentationInfo info)
    {
        Vector3 delta = info.toPosition - info.fromPosition;
        var view = IEntityViewProvider.Resolve().GetView(info.entity, ViewZone.Combatant);
        var combatant = view.GetComponent<CombatantView>();
        ICombatantViewSystem.Resolve().SetAnimation(combatant, CombatantAnimation.Walk);
        await view.transform.MoveTo(info.toPosition, speedMultiplier * delta.magnitude).Play();
        ICombatantViewSystem.Resolve().SetAnimation(combatant, CombatantAnimation.Idle);
    }

    private void OnEnable()
    {
        IStridePresenter.Register(this);
    }

    private void OnDisable()
    {
        IStridePresenter.Reset();
    }
}