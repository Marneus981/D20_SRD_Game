public static class ComponentInjector
//Handles any systems at its own hierarchy level and below, 
//and for those below, it relies on the subfolder level injector.
{
    public static void Inject()
    {
        AbilityScoreInjector.Inject();
        ILevelSystem.Register(new LevelSystem());
        SkillsInjector.Inject();
        IAdventureItemSystem.Register(new AdventureItemSystem());
        INameSystem.Register(new NameSystem());
        IPositionSystem.Register(new PositionSystem());
        ICombatantSystem.Register(new CombatantSystem());
        IPartySystem.Register(new PartySystem());
        IArmorClassSystem.Register(new ArmorClassSystem());
        HealthInjector.Inject();
        IDyingSystem.Register(new DyingSystem());
        ISpeedSystem.Register(new SpeedSystem());
    }
    public static void SetUp()
    {
        AbilityScoreInjector.SetUp();
        IAdventureItemSystem.Resolve().SetUp();
        IArmorClassSystem.Resolve().SetUp();
        ICombatantSystem.Resolve().SetUp();
        IDyingSystem.Resolve().SetUp();
        HealthInjector.SetUp();
        ILevelSystem.Resolve().SetUp();
        INameSystem.Resolve().SetUp();
        IPartySystem.Resolve().SetUp();
        IPositionSystem.Resolve().SetUp();
        SkillsInjector.SetUp();
        ISpeedSystem.Resolve().SetUp();
    }

    public static void TearDown()
    {
        AbilityScoreInjector.TearDown();
        IAdventureItemSystem.Resolve().TearDown();
        IArmorClassSystem.Resolve().TearDown();
        ICombatantSystem.Resolve().TearDown();
        IDyingSystem.Resolve().TearDown();
        HealthInjector.TearDown();
        ILevelSystem.Resolve().TearDown();
        INameSystem.Resolve().TearDown();
        IPartySystem.Resolve().TearDown();
        IPositionSystem.Resolve().TearDown();
        SkillsInjector.TearDown();
        ISpeedSystem.Resolve().TearDown();
    }
}