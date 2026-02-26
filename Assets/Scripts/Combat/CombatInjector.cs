public static class CombatInjector
{
    public static void Inject()
    {
        ICombatResultSystem.Register(new CombatResultSystem());
    }
}