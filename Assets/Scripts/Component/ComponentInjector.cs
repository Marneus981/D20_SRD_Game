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
        SizeInjector.Inject();
        IEntityFilterSystem.Register(new EntityFilterSystem());
    }
}