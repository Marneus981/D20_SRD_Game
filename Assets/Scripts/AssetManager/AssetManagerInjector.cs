public static class AssetManagerInjector
{
    public static void Inject()
    {
        ICombatActionAssetSystem.Register(new CombatActionAssetSystem());
        IEncounterAssetSystem.Register(new EncounterAssetSystem());
        IEntryAssetSystem.Register(new EntryAssetSystem());
        IAncestryAssetSystem.Register(new AncestryAssetSystem());
        IBackgroundAssetSystem.Register(new BackgroundAssetSystem());
        IWeaponAssetSystem.Register(new WeaponAssetSystem());
    }
}