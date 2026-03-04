public static class Injector
//Root level injector
{
    public static void Inject()
    {
        //First
        //-------------------
        ISetUpSystem.Register(new SetUpSystem());
        ITearDownSystem.Register(new TearDownSystem());
        //-------------------
        //Last
        ActionInjector.Inject();
        AssetManagerInjector.Inject();
        CombatInjector.Inject();
        ComponentInjector.Inject();
        DataInjector.Inject();
        DiceRollInjector.Inject();
        EntityInjector.Inject();
        FlowInjector.Inject();
        IEntitySystem.Register(new EntitySystem());
        IGameSystem.Register(new GameSystem());
        IInputSystem.Register(new InputSystem());
        SoloAdventureInjector.Inject();
        BoardInjector.Inject();
    }
}