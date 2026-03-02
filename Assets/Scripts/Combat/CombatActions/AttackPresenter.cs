using UnityEngine;
using Cysharp.Threading.Tasks;

public struct AttackPresentationInfo
{
    public Entity attacker;
    public Entity target;
    public Check result;
}

public interface IAttackPresenter : IDependency<IAttackPresenter>
{
    UniTask Present(AttackPresentationInfo info);
}

public class AttackPresenter : MonoBehaviour, IAttackPresenter
{
    public async UniTask Present(AttackPresentationInfo info)
    {
        var view = IEntityViewProvider.Resolve().GetView(info.attacker, ViewZone.Combatant);
        var combatant = view.GetComponent<CombatantView>();
        await ICombatantViewSystem.Resolve().PlayAnimation(combatant, CombatantAnimation.Attack);
    }

    private void OnEnable()
    {
        IAttackPresenter.Register(this);
    }

    private void OnDisable()
    {
        IAttackPresenter.Reset();
    }
}