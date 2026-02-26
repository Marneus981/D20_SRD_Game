public partial class Data
{
    public string encounterName;
}

public interface IEncounterSystem : IDependency<IEncounterSystem>
{
    void SetName(string name);
    string GetName();
}

public class EncounterSystem : IEncounterSystem
{
    public void SetName(string name)
    {
        IDataSystem.Resolve().Data.encounterName = name;
        if (!string.IsNullOrEmpty(name))////When we set a non-empty Encounter name, then we should clear any Entry name
            IEntrySystem.Resolve().SetName(string.Empty);
    }

    public string GetName()
    {
        return IDataSystem.Resolve().Data.encounterName;
    }
}