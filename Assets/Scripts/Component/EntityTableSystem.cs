/*
Idea:
The basic Component pattern is to simply have a Dictionary that 
maps from an Entity to some kind of data. For example, 
a “Health” component could be a Dictionary that maps from an Entity to an int 
– where the Value is the amount of “hit points” that the Entity currently has. 
Optionally, you could also map to more complex data types such as a struct that 
records both the current hit points and also a max amount of hit points.
*/
public interface IEntityTableSystem<T>
{
    CoreDictionary<Entity, T> Table { get; }
        //Maps from an Entity to something – that Value is the generic T 
        //part of the interface definition.

    void Set(Entity entity, T value);//Data update/creation
    T Get(Entity entity); //Reading data
    bool TryGetValue(Entity entity, out T value);
        //Returns true if the Table has an entry for the given Entity, and the out parameter will hold the Table’s Value if available
    bool Has(Entity entity);
        //Returns whether or not the Table has an entry for the given Entity
    void Remove(Entity entity);
        //Deletes an Entity and its associated data from the Table
}

public abstract class EntityTableSystem<T> : IEntityTableSystem<T>
/*
Note: The Table itself was left as an abstract property. 
Any concrete subclass should determine where the Table comes from. 
If the data it holds should be persisted, it may just be a wrapper 
of a field in the Data of the DataSystem. Otherwise,
it could be defined within the concrete class directly and used only in memory.
*/
{
    public abstract CoreDictionary<Entity, T> Table { get; }

    public EntityTableSystem()
    {
        ISetUpSystem.Resolve().Add(SetUp);
        ITearDownSystem.Resolve().Add(TearDown);
    }
    public virtual void Set(Entity entity, T value)
    {
        Table[entity] = value;
    }

    public virtual T Get(Entity entity)
    {
        T result;
        if (Table.TryGetValue(entity, out result))
            return result;
        return default(T);
    }

    public virtual bool TryGetValue(Entity entity, out T value)
    {
        return Table.TryGetValue(entity, out value);
    }

    public virtual bool Has(Entity entity)
    {
        return Table.ContainsKey(entity);
    }

    public virtual void Remove(Entity entity)
    {
        if (Table.ContainsKey(entity))
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
    /*
    Outside classes won’t know this method exists and can’t call it directly. 
    Subclasses will know about the method and can choose to override it, if needed,
        to do things beyond just deleting its own data.
    */ 
    {
        Remove(entity);
    }
}