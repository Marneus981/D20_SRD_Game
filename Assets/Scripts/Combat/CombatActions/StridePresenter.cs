using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public struct StridePresentationInfo
{
    public Entity entity;
    public List<Point> path;
}

public interface IStridePresenter : IDependency<IStridePresenter>
{
    UniTask Present(StridePresentationInfo info);
}

public class StridePresenter : MonoBehaviour, IStridePresenter
{
    [SerializeField] float moveSpeed = 0.25f;

    public async UniTask Present(StridePresentationInfo info)
    {
        var view = IEntityViewProvider.Resolve().GetView(info.entity, ViewZone.Combatant);
        var combatant = view.GetComponent<CombatantView>();
        ICombatantViewSystem.Resolve().SetAnimation(combatant, CombatantAnimation.Walk);

        var previous = info.path[0];
        for (int i = 1; i < info.path.Count; ++i)
        {
            var next = info.path[i];
            await view.transform.MoveTo(next, moveSpeed).Play();
            previous = next;
        }

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