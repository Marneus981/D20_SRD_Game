[DependencyFactory(typeof(IDataStore))]
public class DataStoreFactory : IDependencyFactory<IDataStore>
{
    public IDataStore Create()
    {
        return new DataStore("GameData");
    }
}