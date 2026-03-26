public struct AttackRollInfo
{
    public Entity attacker;
    public Entity target;
    public int attackRollBonus;
    public int comboCost;
}
public interface IAttackRollSystem : IDependency<IAttackRollSystem>
{
    Check Perform(AttackRollInfo info);
}
[Dependency(typeof(IAttackRollSystem))]
public class AttackRollSystem : IAttackRollSystem
{
    public Check Perform(AttackRollInfo info)
    {
        var turnSystem = ITurnSystem.Resolve();
        var isAttackerCurrent = turnSystem.Current == info.attacker;
        var multipleAttackPenalty = isAttackerCurrent ? turnSystem.AttackCount * info.comboCost : 0;
        var modifier = info.attackRollBonus - multipleAttackPenalty;
        var dc = info.target.ArmorClass;
        var result = ICheckSystem.Resolve().GetResult(modifier, dc);

        if (isAttackerCurrent)
            turnSystem.TakeAction(1, true);

        return result;
    }
}