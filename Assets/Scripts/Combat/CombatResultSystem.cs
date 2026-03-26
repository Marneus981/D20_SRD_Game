using UnityEngine;
using System.Linq;

public enum CombatResult
{
    Victory,
    Defeat
}

public interface ICombatResultSystem : IDependency<ICombatResultSystem>
{
    CombatResult? CheckResult();
}

[Dependency(typeof(ICombatResultSystem))]
public class CombatResultSystem : ICombatResultSystem
{
    public CombatResult? CheckResult()
    {
        var combatants = ICombatantSystem.Resolve().Table;
            
        bool heroAlive = combatants.Any(e => e.Party == Party.Hero && e.HitPoints > 0);//linq check
        if (!heroAlive)
            return CombatResult.Defeat;

        bool enemyAlive = combatants.Any(e => e.Party == Party.Monster && e.HitPoints > 0);//linq check
        if (!enemyAlive)
            return CombatResult.Victory;

        return null;//combat continues
    }
}