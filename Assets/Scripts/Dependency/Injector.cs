public static class Injector
//Root level injector
{
    public static void Inject()
    {
        ComponentInjector.Inject();
        DataInjector.Inject();
        DiceRollInjector.Inject();
        IEntitySystem.Register(new EntitySystem());
        FlowInjector.Inject();
        IGameSystem.Register(new GameSystem());
    }
}