public static class CombatInjector
{
    public static void Inject()
    {
        CombatActionsInjector.Inject();
        ICombatResultSystem.Register(new CombatResultSystem());
    }
}