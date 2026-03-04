using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

public class SoloAdventureAttack : MonoBehaviour, ICombatAction
{
    [SerializeField] int attackRollBonus;
    [SerializeField] int comboCost;
    [SerializeField] DiceRoll damage;
    [SerializeField] string damageType;
    [SerializeField] string material;
    [SerializeField] EntityFilter targetFilter;
    async UniTask<Entity> SelectTarget(Entity entity, List<Entity> targets)
    {
        if (entity.Party == Party.Monster)
            return targets[0];
        else
            return await IEntitySelectionSystem.Resolve().Select(targets);
    }

    public async UniTask Perform(Entity entity)
    {
        var targets = targetFilter.Apply(entity, ITurnSystem.Resolve().InReach);
        if (targets.Count == 0)
            return;
        var attacker = entity;
        var target = await SelectTarget(entity, targets);
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

        // Determine Damage
        DamageInfo damageInfo = new DamageInfo
        {
            target = target,
            damage = 0,
            criticalDamage = 0,
            type = damageType,
            material = material
        };
        switch (attackRoll)
        {
            case Check.CriticalSuccess:
                var critRoll = damage.Roll();
                damageInfo.damage = critRoll;
                damageInfo.criticalDamage = critRoll;
                Debug.Log(string.Format("Critical Hit for {0} Damage!", critRoll * 2));
                break;
            case Check.Success:
                var roll = damage.Roll();
                damageInfo.damage = roll;
                Debug.Log(string.Format("Hit for {0} Damage!", roll));
                break;
            default:
                Debug.Log("Miss");
                break;
        }

        // Apply Damage
        var damageAmount = IDamageSystem.Resolve().Apply(damageInfo);
        var healthInfo = new HealthInfo
        {
            target = target,
            amount = -damageAmount
        };
        await IHealthSystem.Resolve().Apply(healthInfo);
    }
    public bool CanPerform(Entity entity)
    {
        return targetFilter.Apply(entity, ITurnSystem.Resolve().InReach).Count > 0;
    }
}