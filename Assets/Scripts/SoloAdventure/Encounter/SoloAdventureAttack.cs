using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Linq;

public class SoloAdventureAttack : MonoBehaviour, ICombatAction
{
    [SerializeField] int attackRollBonus;
    [SerializeField] int comboCost;
    [SerializeField] DiceRoll damage;

    public async UniTask Perform(Entity entity)
    {
        var attacker = ITurnSystem.Resolve().Current;
        var target = ICombatantSystem.Resolve().Table.First(c => c.Party != attacker.Party);

        // Perform the Attack Roll
        var attackInfo = new AttackRollInfo
        {
            attacker = attacker,
            target = target,
            attackRollBonus = attackRollBonus,
            comboCost = comboCost
        };
        var attackRoll = IAttackRollSystem.Resolve().Perform(attackInfo);

        // Present the Attack
        IAttackPresenter presenter;
        if (IAttackPresenter.TryResolve(out presenter))
        {
            var presentInfo = new AttackPresentationInfo
            {
                attacker = attacker,
                target = target,
                result = attackRoll
            };
            await presenter.Present(presentInfo);
        }

        // TODO: Apply Damage if applicable
        switch (attackRoll)
        {
            case Check.CriticalSuccess:
                Debug.Log(string.Format("Critical Hit for {0} Damage!", damage.Roll() * 2));
                break;
            case Check.Success:
                Debug.Log(string.Format("Hit for {0} Damage!", damage.Roll()));
                break;
            default:
                Debug.Log("Miss");
                break;
        }
    }
}