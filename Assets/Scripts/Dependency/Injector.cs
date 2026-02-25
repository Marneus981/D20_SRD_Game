public static class Injector
//Root level injector
{
    public static void Inject()
    {
        AssetManagerInjector.Inject();
        ComponentInjector.Inject();
        DataInjector.Inject();
        DiceRollInjector.Inject();
        EntityInjector.Inject();
        FlowInjector.Inject();
        IEntitySystem.Register(new EntitySystem());
        IGameSystem.Register(new GameSystem());
        SoloAdventureInjector.Inject();
    }
}