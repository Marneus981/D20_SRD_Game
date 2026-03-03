public static class DataInjector
{
    public static void Inject()
    {
        IDataSerializer.Register(new DataSerializer());
        IDataStore.Register(new DataStore("GameData"));
        IDataSystem.Register(new DataSystem());
    }
    public static void SetUp()
    {
        IDataSerializer.Resolve().SetUp();
        IDataStore.Resolve().SetUp();
        IDataSystem.Resolve().SetUp();
    }

    public static void TearDown()
    {
        IDataSerializer.Resolve().TearDown();
        IDataStore.Resolve().TearDown();
        IDataSystem.Resolve().TearDown();
    }
}