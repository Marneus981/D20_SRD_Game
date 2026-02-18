public partial class Data
/*
Partial Class dfn;
The data isn’t associated with any particular Entity – 
you can just think of it as a bookmark representing which entry to show the user. 
It is just a “name” for an Entry. We will use the name to load an asset via Addressables 
and will then configure our UI based on that loaded asset.
*/
{
    public string entryName;
}
public interface IEntrySystem : IDependency<IEntrySystem>
{
    void SetName(string name);
    string GetName();
}
public class EntrySystem : IEntrySystem
{
    //We implement the “set” and “get” methods by assigning to or reading from the game Data.
    public void SetName(string name)
    {
        IDataSystem.Resolve().Data.entryName = name;
    }

    public string GetName()
    {
        return IDataSystem.Resolve().Data.entryName;
    }
}