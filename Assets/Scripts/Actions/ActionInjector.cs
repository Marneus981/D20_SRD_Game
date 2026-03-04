public static class ActionInjector
{
    public static void Inject()
    {
        ICheckSystem.Register(new CheckSystem());
    }
}