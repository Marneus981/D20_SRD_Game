public static class SoloAdventureInjector
{
    public static void Inject()
    {
        IEncounterSystem.Register(new EncounterSystem());
        IEntrySystem.Register(new EntrySystem());
        ISoloHeroSystem.Register(new SoloHeroSystem());
    }
}