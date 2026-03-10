public partial class Data
{
    public CoreDictionary<Entity, string> background = new CoreDictionary<Entity, string>();
}

public interface IBackgroundSystem : IDependency<IBackgroundSystem>, IEntityTableSystem<string>
{

}

public class BackgroundSystem : EntityTableSystem<string>, IBackgroundSystem
{
    public override CoreDictionary<Entity, string> Table => IDataSystem.Resolve().Data.background;
}

public partial struct Entity
{
    public string Background
    {
        get { return IBackgroundSystem.Resolve().Get(this); }
        set { IBackgroundSystem.Resolve().Set(this, value); }
    }
}