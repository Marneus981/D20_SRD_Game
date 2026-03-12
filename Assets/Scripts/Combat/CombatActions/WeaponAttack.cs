using UnityEngine;

public class WeaponAttack : Attack
{
    [SerializeField] EntityFilter targetFilter;

    protected override int AttackRollBonus(Entity entity)
    {
        var weapon = entity.PrimaryHand;
        var statBonus = StatBonus(entity, weapon);
        var profBonus = ProficiencyBonus(entity, weapon);
        return statBonus + profBonus;
    }

    protected override int ComboCost(Entity entity)
    {
        // TODO: base on weapon
        return 5;
    }

    protected override DiceRoll Damage(Entity entity)
    {
        var weapon = entity.PrimaryHand;
        var roll = weapon.DamageRoll;
        roll.bonus += StatBonus(entity, weapon);
        return roll;
    }

    protected override string DamageType(Entity entity)
    {
        // TODO: base on weapon
        return "slashing";
    }

    protected override string Material(Entity entity)
    {
        // TODO: base on weapon
        return "";
    }

    protected override EntityFilter TargetFilter(Entity entity)
    {
        return targetFilter;
    }

    int StatBonus(Entity entity, Entity weapon)
    {
        AbilityScore.Attribute attribute;
        switch (weapon.WeaponType)
        {
            case WeaponType.Melee:
                attribute = AbilityScore.Attribute.Strength;
                break;
            default: // WeaponType.Ranged:
                attribute = AbilityScore.Attribute.Dexterity;
                break;
        }
        var stat = IAbilityScoreSystem.Resolve().Get(entity, attribute);
        return stat.Modifier;
    }

    int ProficiencyBonus(Entity entity, Entity weapon)
    {
        var result = 0;
        var proficiency = IWeaponProficiencySystem.Resolve().GetProficiency(entity, weapon);
        if (proficiency != Proficiency.Untrained)
            result = (int)proficiency * 2 + entity.Level;
        return result;
    }
}