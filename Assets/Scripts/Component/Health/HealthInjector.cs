public static class HealthInjector
{
    public static void Inject()
    {
        IHealthSystem.Register(new HealthSystem());
        IHitPointSystem.Register(new HitPointSystem());
        IMaxHitPointSystem.Register(new MaxHitPointSystem());
    }
}