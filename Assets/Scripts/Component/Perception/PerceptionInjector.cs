public class PerceptionInjector
{
    public static void Inject()
    {
        IPerceptionProficiencySystem.Register(new PerceptionProficiencySystem());
        IPerceptionSystem.Register(new PerceptionSystem());
    }
}