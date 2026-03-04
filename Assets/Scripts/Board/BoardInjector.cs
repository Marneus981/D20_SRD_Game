public static class BoardInjector
{
    public static void Inject()
    {
        IPathfindingSystem.Register(new PathfindingSystem());
    }

    public static void SetUp()
    {
        IPathfindingSystem.Resolve().SetUp();
    }

    public static void TearDown()
    {
        IPathfindingSystem.Resolve().TearDown();
    }
}