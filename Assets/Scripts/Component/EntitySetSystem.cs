public interface IEntitySetSystem
{
    CoreSet<Entity> Table { get; }

    void Add(Entity entity);
    bool Contains(Entity entity);
    void Remove(Entity entity);
}

public abstract class EntitySetSystem : IEntitySetSystem
{
    public abstract CoreSet<Entity> Table { get; }

    public virtual void Add(Entity entity)
    {
        Table.Add(entity);
    }

    public virtual bool Contains(Entity entity)
    {
        return Table.Contains(entity);
    }

    public virtual void Remove(Entity entity)
    {
        Table.Remove(entity);
    }
    public virtual void SetUp()
    {
        IEntitySystem.Resolve().EntityDestroyed += OnEntityDestroyed;
    }

    public virtual void TearDown()
    {
        IEntitySystem.Resolve().EntityDestroyed -= OnEntityDestroyed;
    }

    protected virtual void OnEntityDestroyed(Entity entity)
    {
        Remove(entity);
    }
}