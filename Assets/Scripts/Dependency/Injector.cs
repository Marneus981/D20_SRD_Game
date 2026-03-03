public static class Injector
//Root level injector
{
    public static void Inject()
    {
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
    }
    public static void SetUp()
    {
        ActionInjector.SetUp();
        AssetManagerInjector.SetUp();
        CombatInjector.SetUp();
        ComponentInjector.SetUp();
        DataInjector.SetUp();
        DiceRollInjector.SetUp();
        EntityInjector.SetUp();
        FlowInjector.SetUp();
        IGameSystem.Resolve().SetUp();
        IInputSystem.Resolve().SetUp();
        SoloAdventureInjector.SetUp();
    }

    public static void TearDown()
    {
        ActionInjector.TearDown();
        AssetManagerInjector.TearDown();
        CombatInjector.TearDown();
        ComponentInjector.TearDown();
        DataInjector.TearDown();
        DiceRollInjector.TearDown();
        EntityInjector.TearDown();
        FlowInjector.TearDown();
        IGameSystem.Resolve().TearDown();
        IInputSystem.Resolve().TearDown();
        SoloAdventureInjector.TearDown();
    }
}