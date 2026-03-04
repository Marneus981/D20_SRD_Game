public static class SoloAdventureInjector
{
    public static void Inject()
    {
        ICombatantAssetSystem.Register(new CombatantAssetSystem());
        ICombatantViewSystem.Register(new CombatantViewSystem());
        IPositionSelectionSystem.Register(new PositionSelectionSystem());
        IEncounterActionsSystem.Register(new EncounterActionsSystem());
        IEncounterSystem.Register(new EncounterSystem());
        IEntrySystem.Register(new EntrySystem());
        ISoloHeroSystem.Register(new SoloHeroSystem());
        IPhysicsSystem.Register(new PhysicsSystem());
    }
}