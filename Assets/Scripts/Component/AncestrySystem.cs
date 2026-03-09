public partial class Data
{
    public CoreDictionary<Entity, string> ancestry = new CoreDictionary<Entity, string>();
}

public interface IAncestrySystem : IDependency<IAncestrySystem>, IEntityTableSystem<string>
{

}

public class AncestrySystem : EntityTableSystem<string>, IAncestrySystem
{
    public override CoreDictionary<Entity, string> Table => IDataSystem.Resolve().Data.ancestry;
}

public partial struct Entity
{
    public string Ancestry
    {
        get { return IAncestrySystem.Resolve().Get(this); }
        set { IAncestrySystem.Resolve().Set(this, value); }
    }
}