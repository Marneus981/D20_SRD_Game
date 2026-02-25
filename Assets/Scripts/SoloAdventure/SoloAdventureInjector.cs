public static class SoloAdventureInjector
{
    public static void Inject()
    {
        IEntrySystem.Register(new EntrySystem());
        ISoloHeroSystem.Register(new SoloHeroSystem());
    }
}