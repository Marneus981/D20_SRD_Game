public static class Injector
//Root level injector
{
    public static void Inject()
    {
        AssetManagerInjector.Inject();
        ComponentInjector.Inject();
        DataInjector.Inject();
        DiceRollInjector.Inject();
        IEntitySystem.Register(new EntitySystem());
        FlowInjector.Inject();
        IGameSystem.Register(new GameSystem());
        SoloAdventureInjector.Inject();
    }
}