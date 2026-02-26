public static class AssetManagerInjector
{
    public static void Inject()
    {
        IEncounterAssetSystem.Register(new EncounterAssetSystem());
        IEntryAssetSystem.Register(new EntryAssetSystem());
    }
}