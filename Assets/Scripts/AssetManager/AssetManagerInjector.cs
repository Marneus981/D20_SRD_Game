public static class AssetManagerInjector
{
    public static void Inject()
    {
        ICombatActionAssetSystem.Register(new CombatActionAssetSystem());
        IEncounterAssetSystem.Register(new EncounterAssetSystem());
        IEntryAssetSystem.Register(new EntryAssetSystem());
    }
    public static void SetUp()
    {
        ICombatActionAssetSystem.Resolve().SetUp();
        IEncounterAssetSystem.Resolve().SetUp();
        IEntryAssetSystem.Resolve().SetUp();
    }

    public static void TearDown()
    {
        ICombatActionAssetSystem.Resolve().TearDown();
        IEncounterAssetSystem.Resolve().TearDown();
        IEntryAssetSystem.Resolve().TearDown();
    }
}