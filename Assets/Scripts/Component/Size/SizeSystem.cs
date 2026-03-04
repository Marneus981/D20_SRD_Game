public enum Size
{
    Tiny,
    Small,
    Medium,
    Large,
    Huge,
    Gargantuan
}

public partial class Data
{
    public CoreDictionary<Entity, Size> size = new CoreDictionary<Entity, Size>();
}

public interface ISizeSystem : IDependency<ISizeSystem>, IEntityTableSystem<Size>
{
    
}

public class SizeSystem : EntityTableSystem<Size>, ISizeSystem
{
    public override CoreDictionary<Entity, Size> Table => IDataSystem.Resolve().Data.size;
}

public partial struct Entity
{
    public Size Size
    {
        get { return ISizeSystem.Resolve().Get(this); }
        set { ISizeSystem.Resolve().Set(this, value); }
    }
}