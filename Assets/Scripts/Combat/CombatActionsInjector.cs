public static class CombatActionsInjector
{
    public static void Inject()
    {
        IStrideSystem.Register(new StrideSystem());
        IAttackRollSystem.Register(new AttackRollSystem());
    }
    public static void SetUp()
    {
        IAttackRollSystem.Resolve().SetUp();
        IStrideSystem.Resolve().SetUp();
    }

    public static void TearDown()
    {
        IAttackRollSystem.Resolve().TearDown();
        IStrideSystem.Resolve().TearDown();
    }
}