using System.Collections.Generic;

public interface IRollInitiativeSystem : IDependency<IRollInitiativeSystem>
{
    void Roll(Entity entity);
    void Roll(IEnumerable<Entity> entities);
}

[Dependency(typeof(IRollInitiativeSystem))]
public class RollInitiativeSystem : IRollInitiativeSystem
{
    public void Roll(Entity entity)
    {
        entity.Initiative = DiceRoll.D20.Roll() + entity.Perception;
    }

    public void Roll(IEnumerable<Entity> entities)
    {
        foreach (var entity in entities)
            Roll(entity);
    }
}