using System.Collections.Generic;

public interface IRoundSystem : IDependency<IRoundSystem>
{
    bool IsComplete { get; }

    void Begin(List<Entity> entities);
    Entity Next();
}

[Dependency(typeof(IRoundSystem))]
public class RoundSystem : IRoundSystem
{
    List<Entity> turnOrder;

    public bool IsComplete { get { return turnOrder.Count == 0; } }

    public void Begin(List<Entity> entities)
    {
        this.turnOrder = entities;
    }

    public Entity Next()
    {
        var result = turnOrder[0];
        turnOrder.RemoveAt(0);
        return result;
    }
}