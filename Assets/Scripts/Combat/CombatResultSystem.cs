using UnityEngine;

public enum CombatResult
{
    Victory,
    Defeat
}

public interface ICombatResultSystem : IDependency<ICombatResultSystem>
{
    CombatResult? CheckResult();
}

public class CombatResultSystem : ICombatResultSystem
{
    public CombatResult? CheckResult()
    {
        if (Input.GetKeyUp(KeyCode.V))//Placeholder: simulate victory on V key press
            return CombatResult.Victory;
        if (Input.GetKeyUp(KeyCode.D))//Placeholder: simulate defeat on D key press
            return CombatResult.Defeat;
        return null;
    }
}