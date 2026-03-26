public partial class Data
//Partial implementation of Data, CoreDictionary that maps from an Entity to a string.
//We created the table here because this component should be persisted.
{
    public CoreDictionary<Entity, string> name = new CoreDictionary<Entity, string>();
}

public interface INameSystem : IDependency<INameSystem>, IEntityTableSystem<string>
/*
Note: Inherits from IDependency so that we can inject any conforming system we wish.
Inherits from IEntityTableSystem so that we have all of the basic CRUD operations 
and a reference to the Table that is managed by the system.*/
{

}
[Dependency(typeof(INameSystem))]
public class NameSystem : EntityTableSystem<string>, INameSystem
{
    public override CoreDictionary<Entity, string> Table => IDataSystem.Resolve().Data.name;
}

public partial struct Entity
/*
Note: This partial definition of Entity exposes a Name property that wraps 
the relevant methods from the injected INameSystem. As an additional bonus, 
when debugging your IDE may automatically associate potentially helpful information 
for you (In this case, Name and ID). 

*/
{
    public string Name
    {
        get { return INameSystem.Resolve().Get(this); }
        set { INameSystem.Resolve().Set(this, value); }
    }
}