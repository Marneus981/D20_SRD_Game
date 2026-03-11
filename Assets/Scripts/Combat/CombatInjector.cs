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
        IRollInitiativeSystem.Register(new RollInitiativeSystem());
    }
}