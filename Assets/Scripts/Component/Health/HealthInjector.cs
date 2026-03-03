public static class HealthInjector
{
    public static void Inject()
    {
        IHealthSystem.Register(new HealthSystem());
        IHitPointSystem.Register(new HitPointSystem());
        IMaxHitPointSystem.Register(new MaxHitPointSystem());
    }
    public static void SetUp()
    {
        IHealthSystem.Resolve().SetUp();
        IHitPointSystem.Resolve().SetUp();
        IMaxHitPointSystem.Resolve().SetUp();
    }

    public static void TearDown()
    {
        IHealthSystem.Resolve().TearDown();
        IHitPointSystem.Resolve().TearDown();
        IMaxHitPointSystem.Resolve().TearDown();
    }
}