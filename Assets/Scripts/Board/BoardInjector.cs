public static class BoardInjector
{
    public static void Inject()
    {
        IPathfindingSystem.Register(new PathfindingSystem());
    }

}