public enum Check
{
    CriticalFailure,
    Failure,
    Success,
    CriticalSuccess
}
public interface ICheckSystem : IDependency<ICheckSystem>
{
    Check GetResult(int modifier, int difficultyCheck);
}
[Dependency(typeof(ICheckSystem))]
public class CheckSystem : ICheckSystem
{
    public Check GetResult(int modifier, int difficultyCheck)
    {
        var roll = DiceRoll.D20.Roll();
        var attempt = roll + modifier;

        Check result;
        if (attempt >= difficultyCheck + 10)
            result = Check.CriticalSuccess;
        else if (attempt >= difficultyCheck)
            result = Check.Success;
        else if (attempt <= difficultyCheck - 10)
            result = Check.CriticalFailure;
        else
            result = Check.Failure;

        if (roll == 20 && result < Check.CriticalSuccess)
            result += 1;
        else if (roll == 1 && result > Check.CriticalFailure)
            result -= 1;

        return result;
    }
}