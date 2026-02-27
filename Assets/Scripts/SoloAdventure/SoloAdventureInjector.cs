public static class SoloAdventureInjector
{
    public static void Inject()
    {
        ICombatantViewSystem.Register(new CombatantViewSystem());
        IPositionSelectionSystem.Register(new PositionSelectionSystem());
        IEncounterActionsSystem.Register(new EncounterActionsSystem());
        IEncounterSystem.Register(new EncounterSystem());
        IEntrySystem.Register(new EntrySystem());
        ISoloHeroSystem.Register(new SoloHeroSystem());
    }
}