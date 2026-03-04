public static class SizeInjector
{
    public static void Inject()
    {
        IReachSystem.Register(new ReachSystem());
        ISpaceSystem.Register(new SpaceSystem());
        ISizeSystem.Register(new SizeSystem());
    }
}