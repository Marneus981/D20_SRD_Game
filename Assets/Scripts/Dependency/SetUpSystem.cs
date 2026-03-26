public interface ISetup
{
    void SetUp();
}

public interface ISetUpSystem : IDependency<ISetUpSystem>
{
    void SetUp();
}

[Dependency(typeof(ISetUpSystem))]
public class SetUpSystem : ISetUpSystem
{
    public void SetUp()
    {
        var systems = DependencyCollection<ISetup>.Collection;
        foreach (var system in systems)
            system.SetUp();
    }
}