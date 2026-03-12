public static class EquipmentInjector
{
    public static void Inject()
    {
        IPrimaryHandSystem.Register(new PrimaryHandSystem());
        WeaponInjector.Inject();
    }
}