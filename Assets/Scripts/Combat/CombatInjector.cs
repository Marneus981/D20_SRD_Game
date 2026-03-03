public static class CombatInjector
{
    public static void Inject()
    {
        CombatActionsInjector.Inject();
        ICombatResultSystem.Register(new CombatResultSystem());
        ICombatantSystem.Register(new CombatantSystem());
        IRoundSystem.Register(new RoundSystem());
        ITurnSystem.Register(new TurnSystem());
        DamageInjector.Inject();
    }
    public static void SetUp()
    {
        CombatActionsInjector.SetUp();
        ICombatantSystem.Resolve().SetUp();
        ICombatResultSystem.Resolve().SetUp();
        DamageInjector.SetUp();
        IRoundSystem.Resolve().SetUp();
        ITurnSystem.Resolve().SetUp();
    }

    public static void TearDown()
    {
        CombatActionsInjector.TearDown();
        ICombatantSystem.Resolve().TearDown();
        ICombatResultSystem.Resolve().TearDown();
        DamageInjector.TearDown();
        IRoundSystem.Resolve().TearDown();
        ITurnSystem.Resolve().TearDown();
    }
}