public interface ITearDown
{
    void TearDown();
}

public interface ITearDownSystem : IDependency<ITearDownSystem>
{
    void TearDown();
}

[Dependency(typeof(ITearDownSystem))]
public class TearDownSystem : ITearDownSystem
{
    public void TearDown()
    {
        var systems = DependencyCollection<ITearDown>.Collection;
        foreach (var system in systems)
            system.TearDown();
    }
}