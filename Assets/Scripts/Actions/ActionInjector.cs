public static class ActionInjector
{
    public static void Inject()
    {
        ICheckSystem.Register(new CheckSystem());
    }
    public static void SetUp()
    {
        ICheckSystem.Resolve().SetUp();
    }

    public static void TearDown()
    {
        ICheckSystem.Resolve().TearDown();
    }
}