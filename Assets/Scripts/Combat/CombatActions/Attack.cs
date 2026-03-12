using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public abstract class Attack : MonoBehaviour, ICombatAction
{
    protected abstract int AttackRollBonus(Entity entity);
    protected abstract int ComboCost(Entity entity);
    protected abstract DiceRoll Damage(Entity entity);
    protected abstract string DamageType(Entity entity);
    protected abstract string Material(Entity entity);
    protected abstract EntityFilter TargetFilter(Entity entity);

    public bool CanPerform(Entity entity)
    {
        return TargetFilter(entity).Apply(entity, ITurnSystem.Resolve().InReach).Count > 0;
    }

    public async UniTask Perform(Entity entity)
    {
        var targets = TargetFilter(entity).Apply(entity, ITurnSystem.Resolve().InReach);
        if (targets.Count == 0)
            return;

        var attacker = entity;
        var target = await SelectTarget(entity, targets);

        // Perform the Attack Roll
        var attackInfo = new AttackRollInfo
        {
            attacker = attacker,
            target = target,
            attackRollBonus = AttackRollBonus(entity),
            comboCost = ComboCost(entity)
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
            type = DamageType(entity),
            material = Material(entity)
        };
        switch (attackRoll)
        {
            case Check.CriticalSuccess:
                var critRoll = Damage(entity).Roll();
                damageInfo.damage = critRoll;
                damageInfo.criticalDamage = critRoll;
                Debug.Log(string.Format("Critical Hit for {0} Damage!", critRoll * 2));
                break;
            case Check.Success:
                var roll = Damage(entity).Roll();
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

    async UniTask<Entity> SelectTarget(Entity entity, List<Entity> targets)
    {
        if (entity.Party == Party.Monster)
            return targets[0];
        else
            return await IEntitySelectionSystem.Resolve().Select(targets);
    }
}